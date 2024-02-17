using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface IGenericRepository<T> 
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> InsertWithReturnAsync(T entity);

        Task<int> Delete(T entity);

        Task InsertWithNoReturnAsync(T entity);

    }

}

