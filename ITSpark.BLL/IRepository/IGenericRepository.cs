using ITSpark.BLL.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.BLL.IRepository
{
    public interface IGenericRepository<Model> where Model : class
    {
        public Task AddAsync(Model model);
        public void  UpdateAsync(Model model);
        public void DeleteAsync(Model model);

        public Task<Model?> GetByIdAsync(int id);
        public Task<Model?> GetByIdWithSpecificationAsync(ISpecification<Model> specification);
        public Task<IReadOnlyList<Model>> GetAllAsync();
        public Task<IReadOnlyList<Model>> GetAllWithSpecAsync(ISpecification<Model> specification);
    }
}
