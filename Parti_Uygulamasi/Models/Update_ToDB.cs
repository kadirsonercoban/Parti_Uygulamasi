using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public class Update_ToDB
    {
        //çalışan günceller
        public static string UpdatePW(PartyWorkerModel pw)
        {
            try
            {
                NpgsqlConnection conn = DatabaseManager.GetConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update_partyworker";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
              
                Random random = new Random();
                byte[] randomBytes = new byte[10];
                random.NextBytes(randomBytes);

                cmd.Parameters.AddWithValue("id_", NpgsqlTypes.NpgsqlDbType.Integer, pw.Id);
                cmd.Parameters.AddWithValue("name_", NpgsqlTypes.NpgsqlDbType.Varchar, pw.FirstName);
                cmd.Parameters.AddWithValue("surname_", NpgsqlTypes.NpgsqlDbType.Varchar, pw.LastName);
                cmd.Parameters.AddWithValue("title_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("city_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.City ?? DBNull.Value);
                cmd.Parameters.AddWithValue("position_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Position ?? DBNull.Value);
                cmd.Parameters.AddWithValue("intro_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Introduction ?? DBNull.Value);
                cmd.Parameters.AddWithValue("phone_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("email_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("webpage_", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.WebPage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("photo_", NpgsqlTypes.NpgsqlDbType.Bytea, (object)randomBytes ?? DBNull.Value);

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
