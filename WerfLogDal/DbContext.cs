using SQLite;
using System;
using System.Collections.Generic;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;
using WerfLogDal.Repositories;


namespace WerfLogDal
{
    public class DbContext :IDisposable
    {

        private SQLiteConnection _connection;
        private bool _initialized = false;


        // Methode om de SQLiteAsyncConnection te verkrijgen
        //public SQLiteConnection GetConnection() => _connection;

        public DbContext()
        {
            
            _connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
         
        }

        private void Init()
        {
            if (!_initialized)
            {

                // Activeer foreign key support
                _connection.Execute("PRAGMA foreign_keys = ON");

                // Creëer de tabel 'Werven'
                _connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS Werf (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Naam TEXT CHECK(length(Naam) <= 150));
                                ");

                // Creëer de tabel 'Notities'
                _connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS Notitie (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Tekst TEXT,
                                Datum DATETIME,
                                WerfId INTEGER,
                                FOREIGN KEY (WerfId) REFERENCES Werf(Id) ON DELETE CASCADE); 
                                ");

                // Creëer de tabel 'Tijdregistraties'
                _connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS Tijdregistratie (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                StartTijd DATETIME,
                                StopTijd DATETIME,
                                WerfId INTEGER,
                                FOREIGN KEY (WerfId) REFERENCES Werf(Id) ON DELETE SET NULL); 
                                ");

                // Creëer de tabel 'ProjectOverleg'
                _connection.Execute(@"
                                CREATE TABLE IF NOT EXISTS ProjectOverleg (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Datum DATETIME,
                                Tekst TEXT);
                                ");

                _initialized = true;
            }

            
        }

        public SQLiteConnection GetConnection()
        {
            _connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            Init();
            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
