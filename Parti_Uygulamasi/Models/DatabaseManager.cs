using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public static class DatabaseManager
    {
        private static string connString = "Host=localhost;Username=postgres;Password=12345;Database=political_party_db";

        public static NpgsqlConnection GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            return connection;
        }
    }
}
