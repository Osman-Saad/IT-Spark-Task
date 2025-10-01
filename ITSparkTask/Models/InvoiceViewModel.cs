using ITSpark.DAL.Models;

namespace ITSparkTask.PL.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}
