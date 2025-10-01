using AutoMapper;
using ITSpark.BLL.IRepository;
using ITSpark.BLL.Specification;
using ITSpark.DAL.Models;
using ITSparkTask.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITSparkTask.PL.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var invoiceSpec = new InvoiceSpecification();
            var invoices = await unitOfWork.GetRepository<Invoice>().GetAllWithSpecAsync(invoiceSpec);
            var invoicesVM = mapper.Map<IEnumerable<InvoiceViewModel>>(invoices);
            return View(invoicesVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.InvoiceTypes = Enum.GetNames(typeof(InvoiceType));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel invoiceVM)
        {
            if (ModelState.IsValid)
            {
                var invoice = mapper.Map<Invoice>(invoiceVM);
                await unitOfWork.GetRepository<Invoice>().AddAsync(invoice);
                var result = await unitOfWork.CompleteAsync();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return BadRequest();
            }
            return View(invoiceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var invoiceSpec = new InvoiceSpecification(id);
            var invoice = await unitOfWork.GetRepository<Invoice>().GetByIdWithSpecificationAsync(invoiceSpec);
            if (invoice == null)
                return NotFound();
            var invoiceVM = mapper.Map<InvoiceViewModel>(invoice);
            return View(invoiceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var invoiceSpec = new InvoiceSpecification(id);
            var invoice = await unitOfWork.GetRepository<Invoice>().GetByIdWithSpecificationAsync(invoiceSpec);
            if (invoice == null)
                return NotFound();
            var invoiceVM = mapper.Map<InvoiceViewModel>(invoice);
            ViewBag.InvoiceTypes = Enum.GetNames(typeof(InvoiceType));
            return View(invoiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(InvoiceViewModel invoiceVM, int id)
        {
            if (id != invoiceVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var invoiceSpec = new InvoiceSpecification(id);
                var invoice = await unitOfWork.GetRepository<Invoice>()
                                               .GetByIdWithSpecificationAsync(invoiceSpec);

                if (invoice == null)
                    return NotFound();

                invoice.Date = invoiceVM.Date;
                invoice.Type = Enum.Parse<InvoiceType>(invoiceVM.Type);

                var updatedItemIds = invoiceVM.Items.Where(i => i.Id > 0).Select(i => i.Id).ToList();
                var itemsToRemove = invoice.Items.Where(i => !updatedItemIds.Contains(i.Id)).ToList();

                foreach (var item in itemsToRemove)
                    invoice.Items.Remove(item);

                foreach (var itemVM in invoiceVM.Items)
                {
                    if (itemVM.Id > 0)
                    {
                        var existingItem = invoice.Items.FirstOrDefault(i => i.Id == itemVM.Id);
                        if (existingItem != null)
                        {
                            existingItem.Name = itemVM.Name;
                            existingItem.Quantity = itemVM.Quantity;
                            existingItem.UnitPrice = itemVM.UnitPrice;
                        }
                    }
                    else
                    {
                        invoice.Items.Add(new Item
                        {
                            Name = itemVM.Name,
                            Quantity = itemVM.Quantity,
                            UnitPrice = itemVM.UnitPrice,
                            InvoiceId = invoice.Id
                        });
                    }
                }

                unitOfWork.GetRepository<Invoice>().UpdateAsync(invoice);
                var result = await unitOfWork.CompleteAsync();

                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }

            return View(invoiceVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var invoiceSpec = new InvoiceSpecification(id);
            var invoice = await unitOfWork.GetRepository<Invoice>().GetByIdWithSpecificationAsync(invoiceSpec);
            if (invoice == null)
                return NotFound();
            var invoiceVM = mapper.Map<InvoiceViewModel>(invoice);
            return View(invoiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(InvoiceViewModel invoiceVM, int id)
        {
            if (id != invoiceVM.Id)
                return BadRequest();
            var invoiceSpec = new InvoiceSpecification(id);
            var invoice = await unitOfWork.GetRepository<Invoice>().GetByIdWithSpecificationAsync(invoiceSpec);
            if (invoice == null)
                return NotFound();
            unitOfWork.GetRepository<Invoice>().DeleteAsync(invoice);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View(invoiceVM);
        }
    }
}
