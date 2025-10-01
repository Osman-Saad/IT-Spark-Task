using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.BLL.IRepository
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IGenericRepository<Model> GetRepository<Model>() where Model : class;
        public Task<int> CompleteAsync();
    }
}
