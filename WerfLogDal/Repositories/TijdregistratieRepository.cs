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

                // Eerst de StartTijd ophalen voor de betreffende tijdregistratie.
                var tijdregistratie = await connection.Table<Tijdregistratie>().Where(t => t.Id == id).FirstOrDefaultAsync();
                if (tijdregistratie == null)
                {
                    throw new Exception($"Geen Tijdregistratie gevonden met id: {id}.");
                }

                // Bereken de TotaleTijd in minuten, als de EindTijd niet null is.
                TimeSpan duur = stopTijd - tijdregistratie.StartTijd;
                int totaleTijdInMinuten = (int)duur.TotalMinutes;

                // Update zowel de StopTijd als de TotaleTijd in de database.
                string query = "UPDATE Tijdregistratie SET StopTijd = @stopTijd, TotaleTijd = @totaleTijdInMinuten WHERE Id = @id";

                // Voer de update uit.
                int rowsAffected = await connection.ExecuteAsync(query,stopTijd, totaleTijdInMinuten, id);

                if (rowsAffected == 0)
                {
                    throw new Exception($"Update mislukt voor Tijdregistratie met id: {id}.");
                }
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het bijwerken van de StopTijd en TotaleTijd in de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het bijwerken van de StopTijd en TotaleTijd.", ex);
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

        public async Task<int> HaalTotaalUrenOpPerMaand(int maand, int jaar)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                var query = @"
                                SELECT SUM(TotaleTijd) AS TotaleDuurInMinuten
                                FROM Tijdregistratie
                                WHERE strftime('%Y', datetime(StartTijd / 10000000 - 62135596800, 'unixepoch')) = @Jaar 
                                AND strftime('%m', datetime(StartTijd / 10000000 - 62135596800, 'unixepoch')) = @Maand;";

                // Bereid het command voor en voer het uit met de parameters
                var totaleDuurInMinuten = await connection.ExecuteScalarAsync<int?>(query, jaar.ToString(), maand.ToString("00"));


                // Controleer of er een resultaat is en bereken het totaal aantal uren.
                if (totaleDuurInMinuten.HasValue)
                {
                    return totaleDuurInMinuten.Value; // Als je de duur in minuten hebt, en je wilt uren, moet je misschien delen door 60.
                }
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het ophalen van het totaal aantal uren uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van het totaal aantal uren.", ex);
            }

            return 0; // Geen uren gevonden voor de opgegeven maand en jaar
        }


        public async Task<List<Tijdregistratie>> HaalAlleTijdregistratiesOpPerMaand(int maand, int jaar)
        {
            try
            {
                SQLiteAsyncConnection connection = await _context.GetConnectionAsync();

                var query = @"
                     SELECT *, datetime(StartTijd / 10000000 - 62135596800, 'unixepoch') AS StartDateTime
                     FROM Tijdregistratie
                     WHERE strftime('%Y', datetime(StartTijd / 10000000 - 62135596800, 'unixepoch')) = @Jaar 
                     AND strftime('%m', datetime(StartTijd / 10000000 - 62135596800, 'unixepoch')) = @Maand
                    ORDER BY StartTijd ASC;
                     ";

                // Bereid het commando voor en voer het uit.
                var tijdregistraties = await connection.QueryAsync<Tijdregistratie>(query, jaar.ToString(), maand.ToString("00"));



                // Geef de gevonden tijdregistraties terug.
                return tijdregistraties;
            }
            catch (SQLiteException ex)
            {
                // Specifieke afhandeling voor SQLite gerelateerde fouten.
                throw new DatabaseException("Fout tijdens het ophalen van de tijdregistraties uit de database.", ex);
            }
            catch (Exception ex)
            {
                // Afhandeling van andere onverwachte fouten.
                throw new Exception("Een onverwachte fout is opgetreden tijdens het ophalen van de tijdregistraties.", ex);
            }
        }


    }
}
