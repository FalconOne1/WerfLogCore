using SQLite;
using WerfLogDal.Exceptions;

namespace WerfLogDal
{
    public class DbContext /*:IDisposable*/
    {
        private SQLiteAsyncConnection _connection;
        private bool _initialized = false;

        public DbContext()
        {
            //_/*connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);*/
              //GetConnectionAsync();
        }
        private async Task Init()
        {
            try
            {
                if (!_initialized)
                {

                    // Activeer foreign key support
                    await _connection.ExecuteAsync("PRAGMA foreign_keys = ON");

                    // Creëer de tabel 'Werven'
                    await _connection.ExecuteAsync(@"
                                CREATE TABLE IF NOT EXISTS Werf (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Naam TEXT CHECK(length(Naam) <= 150),
                                IsActief INTEGER DEFAULT 1);
                                ");

                    // Creëer de tabel 'Notities'
                    await _connection.ExecuteAsync(@"
                                CREATE TABLE IF NOT EXISTS Notitie (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Tekst TEXT,
                                Datum DATETIME,
                                WerfId INTEGER,
                                FOREIGN KEY (WerfId) REFERENCES Werf(Id) ON DELETE CASCADE); 
                                ");

                    // Creëer de tabel 'Tijdregistraties'
                    await _connection.ExecuteAsync(@"
                                CREATE TABLE IF NOT EXISTS Tijdregistratie (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTijd DATETIME,
                                StopTijd DATETIME,
                                TotaleTijd INTEGER,
                                WerfNaamRegistratie TEXT,
                                WerfId INTEGER,
                                FOREIGN KEY (WerfId) REFERENCES Werf(Id) ON DELETE SET NULL); 
                                ");

                    // Creëer de tabel 'ProjectOverleg'
                    await _connection.ExecuteAsync(@"
                                CREATE TABLE IF NOT EXISTS ProjectOverleg (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Datum DATETIME,
                                Tekst TEXT);
                                ");

                    _initialized = true;
                }
            }
            catch (SQLiteException ex)
            {
                throw new DatabaseException("Fout bij aanmaken van databank.", ex);
            }

            catch (Exception ex)
            {
                throw new Exception("Algemene fout bij aanmaken databank.", ex);
            }  
        }
        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                }
                await Init();
                return _connection;
            }
            catch (SQLiteException ex)
            {
                throw new DatabaseException("Fout bij verbinden met databank.", ex);
            }

            catch (Exception ex)
            {
               throw new Exception("Algemene Fout bij verbinden met databank.", ex);
            }
        }
    }
}
