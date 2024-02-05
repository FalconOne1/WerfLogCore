using SQLite;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;


namespace WerfLogDal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context) 
        {
            _context = context;
        }

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertWithNoReturn(T entity)
        {
            SQLiteConnection connection = _context.GetConnection();

            connection.Insert(entity);

          
        }

        public T InsertWithReturn(T entity)
        {
            SQLiteConnection connection = _context.GetConnection();

            // Insert the entity into the database.
            int rows = connection.Insert(entity);

            // Retrieve the last insert id.
            var lastId = connection.ExecuteScalar<int>("SELECT last_insert_rowid();");

            // Assign the retrieved id to the entity's Id property.
            entity.Id = lastId;

            // Return the entity with the assigned Id.
            return entity;
        }






        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
