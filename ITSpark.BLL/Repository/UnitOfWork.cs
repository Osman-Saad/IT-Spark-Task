using ITSpark.BLL.IRepository;
using ITSpark.DAL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITSparkDbContext dbContext;
        private readonly Hashtable repositories;

        public UnitOfWork(ITSparkDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() =>
            await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() =>
            await dbContext.DisposeAsync();

        public IGenericRepository<Model> GetRepository<Model>() where Model : class
        {
            var type = typeof(Model).Name;
            if (!repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<Model>(dbContext);
                repositories.Add(type, repo);
            }
            return repositories[type] as IGenericRepository<Model>;
        }
    }
}
