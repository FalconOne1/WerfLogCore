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
        List<T> GetAll();
        T GetById(int id);
        T InsertWithReturn(T entity);
        int Update(T entity);
        int Delete(T entity);

        void InsertWithNoReturn(T entity);

    }

}

