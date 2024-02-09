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
    public class TijdregistratieRepository : GenericRepository<Tijdregistratie>,  ITijdregistratieRepository
    {
        public TijdregistratieRepository(DbContext connection) : base (connection)
        {

        }

      

        public async Task UpdateStopTijdById(int id, DateTime stopTijd)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                string query = "UPDATE Tijdregistratie SET StopTijd = @stopTijd WHERE Id = @id";

                // Voer de update uit.
                int rowsAffected = await connection.ExecuteAsync(query,stopTijd, id);

                if (rowsAffected == 0)
                {
                    throw new Exception($"Geen Tijdregistratie gevonden met id: {id}.");
                }
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het bijwerken van de StopTijd in de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het bijwerken van de StopTijd.", ex);
            }
        }

        public async Task<Tijdregistratie> GetEmptyStopDateTimeAsync()
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                // Gebruik LIMIT 1 om ervoor te zorgen dat er maximaal één resultaat terugkomt.
                string query = "SELECT * FROM Tijdregistratie WHERE StopTijd IS NULL LIMIT 1";

                var tijdregistratie = await connection.FindWithQueryAsync<Tijdregistratie>(query);

                if (tijdregistratie == null)
                {
                    return null;
                }

                return tijdregistratie;
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het ophalen van de actieve tijdregistratie uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van de actieve tijdregistratie.", ex);
            }
        }

        public async Task<Tijdregistratie> GetTijdregistratieById(int id)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                // Query de entiteit met de opgegeven id uit de database.
                Tijdregistratie tijdregistratie = await connection.FindAsync<Tijdregistratie>(id);

                // Retourneer de gevonden entiteit.
                return tijdregistratie;
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het ophalen van de entiteit uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van de entiteit.", ex);
            }
        }


    }
}
