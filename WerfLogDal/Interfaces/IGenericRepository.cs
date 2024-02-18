namespace WerfLogDal.Interfaces
{
    public interface IGenericRepository<T>
    {


        Task<T> InsertWithReturnAsync(T entity);

        Task<int> Delete(T entity);

        Task InsertWithNoReturnAsync(T entity);

    }

}

