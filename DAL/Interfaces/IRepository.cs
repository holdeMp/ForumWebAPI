using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> FindAll();
        void Update(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        void Delete(TEntity entity);
    }
}
