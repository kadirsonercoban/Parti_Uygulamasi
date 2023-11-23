using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public static class Deletes_FromDB
    {
        public static string DeletePW(int pw_id)
        {
            try
            {
                NpgsqlConnection conn = DatabaseManager.GetConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete_pw_by_id";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Prosedüre parametreleri ekleyin
                cmd.Parameters.AddWithValue("id_", NpgsqlTypes.NpgsqlDbType.Integer, pw_id);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {

                string hata = e.Message;
                Console.WriteLine(hata);
                return "error";
            }
            return "ok";
        }

    }
}
