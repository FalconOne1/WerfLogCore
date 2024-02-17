using SQLite;
using WerfLogDal.Exceptions;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;


namespace WerfLogDal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        internal readonly DbContext _context;

        public GenericRepository(DbContext context) 
        {
            _context = context;
        }


        public async Task InsertWithNoReturnAsync(T entity)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();
                connection.InsertAsync(entity);
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het invoegen van de entiteit in de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het invoegen van de entiteit.", ex);
            }

        }

        public async Task<T> InsertWithReturnAsync(T entity)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                // Insert the entity into the database.
                int rowsAffected = await connection.InsertAsync(entity);

                if (rowsAffected > 0)
                {
                    // Retrieve the last insert id.
                    var lastId = await connection.ExecuteScalarAsync<int>("SELECT last_insert_rowid();");

                    // Assign the retrieved id to the entity's Id property.
                    entity.Id = lastId;

                    // Return the entity with the assigned Id.
                    return entity;
                }
                else
                {
                  throw new DatabaseException("Geen rijen toegevoegd aan de database.");
                }
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het invoegen van de entiteit in de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het invoegen van de entiteit.", ex);
            }
        }

        //public async Task<int> Delete(T entity)
        //{
        //    try
        //    {
        //        SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

        //        // Verwijder de entiteit uit de database
        //        int rowsAffected = await connection.DeleteAsync(entity);

        //        if (rowsAffected > 0)
        //        {
        //            return rowsAffected;
        //        }
        //        else
        //        {
        //            throw new DatabaseException("Geen rijen verwijderd uit de database.");
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        // Specifieke afhandeling voor SQLite gerelateerde fouten.
        //        throw new DatabaseException("Fout tijdens het verwijderen van de entiteit uit de database.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Afhandeling van andere onverwachte fouten.
        //        throw new Exception("Een onverwachte fout is opgetreden tijdens het verwijderen van de entiteit.", ex);
        //    }
        //}

        public async Task<int> Delete(T entity)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                // Stel een SQL-delete-query samen
                string query = $"DELETE FROM {typeof(T).Name} WHERE Id = @id";
                int rowsAffected = await connection.ExecuteAsync(query, entity.Id );

                if (rowsAffected > 0)
                {
                    return rowsAffected;
                }
                else
                {
                    throw new DatabaseException("Geen rijen verwijderd uit de database.");
                }
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het verwijderen van de entiteit uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het verwijderen van de entiteit.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            //try
            //{
            //    SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

            //    // Verwijder de WHERE-clausule om alle rijen op te halen

            //    string query = $"SELECT * FROM {typeof(T).Name}";
            //    List<T> entities = await connection.QueryAsync<T>(query);

            //    return entities;
            //}
            //catch (SQLiteException ex)
            //{
            //    // Specifieke afhandeling voor SQLite gerelateerde fouten.
            //    throw new Exception("Fout tijdens het ophalen van gegevens uit de database.", ex);
            //}
            //catch (Exception ex)
            //{
            //    // Afhandeling van andere onverwachte fouten.
            //    throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van gegevens.", ex);
            //}

            throw new NotImplementedException();
        }


        public async Task<T>GetById(int id) 
        {
            throw new NotImplementedException();
        }

    }
}
