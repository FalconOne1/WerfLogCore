using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;

namespace WerfLogDal.Repositories
{
    public class WerfRepository : GenericRepository<Werf>, IWerfRepository
    {
        public WerfRepository(DbContext connection) : base(connection)
        {
        }


        public async Task<List<Werf>> GetAllWervenAsync()
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                //Controle of item niet "verwijdert is" -> Where 0

                string query = $"SELECT * FROM Werf WHERE IsActief != 0";
                List<Werf> entities = await connection.QueryAsync<Werf>(query);

                return entities;
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new Exception("Fout tijdens het ophalen van gegevens uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van gegevens.", ex);
            }
        }


    }
}
