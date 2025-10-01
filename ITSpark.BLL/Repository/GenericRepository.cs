using ITSpark.BLL.IRepository;
using ITSpark.BLL.Specification;
using ITSpark.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.BLL.Repository
{
    public class GenericRepository<Model> : IGenericRepository<Model> where Model : class
    {
        private readonly ITSparkDbContext dbContext;

        public GenericRepository(ITSparkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Model model) =>
           await dbContext.Set<Model>().AddAsync(model);

        public void DeleteAsync(Model model) =>
             dbContext.Set<Model>().Remove(model);

        public async Task<IReadOnlyList<Model>> GetAllAsync() =>
            await dbContext.Set<Model>().ToListAsync();

        public async Task<IReadOnlyList<Model>> GetAllWithSpecAsync(ISpecification<Model> specification) =>
            await ApplySpecification(specification).ToListAsync();

        public async Task<Model?> GetByIdAsync(int id) =>
            await dbContext.Set<Model>().FindAsync(id);

        public async Task<Model?> GetByIdWithSpecificationAsync(ISpecification<Model> specification)=>
           await ApplySpecification(specification).FirstOrDefaultAsync();

        public void UpdateAsync(Model model) =>
            dbContext.Set<Model>().Update(model);

        private IQueryable<Model> ApplySpecification(ISpecification<Model> specification)
        {
            return SpecificationEvluator<Model>.GetQuery(dbContext.Set<Model>(), specification);
        }
    }
}
