using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogDal.Exceptions;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;

namespace WerfLogDal.Repositories
{
    public class NotitieRepository : GenericRepository<Notitie>, INotitieRepository
    {
        public NotitieRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Notitie>> GetAllNotitiesByWerfIdAsync(int Id)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                string query = "SELECT * FROM Notitie WHERE WerfId = ?";

                List<Notitie> result = await connection.QueryAsync<Notitie>(query, Id);
                return result;
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het ophalen van de notities in de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van de notities.", ex);
            }

        }
    }
}
 