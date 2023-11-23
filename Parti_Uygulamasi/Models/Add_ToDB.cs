using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public static class Add_ToDB
    {
        public static string AddPartyWorker(PartyWorkerModel pw)
        {
            string situation = "";
            try
            {
                //if (newAdditionModel.Photo != null && newAdditionModel.Photo.Length > 0)
                //{
                //byte[] photo = ConvertJobs.ConvertIFormFileToBytes(newAdditionModel.Photo);
                NpgsqlConnection conn = DatabaseManager.GetConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert_pw";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Random random = new Random();
                byte[] randomBytes = new byte[10];
                random.NextBytes(randomBytes);

                // Prosedüre parametreleri ekleyin
                cmd.Parameters.AddWithValue("_pw_name", NpgsqlTypes.NpgsqlDbType.Varchar, pw.FirstName);
                cmd.Parameters.AddWithValue("_pw_surname", NpgsqlTypes.NpgsqlDbType.Varchar, pw.LastName);
                cmd.Parameters.AddWithValue("_pw_title", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_city", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.City ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_position", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Position ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_intro", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Introduction ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_phone", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_email", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_webpage", NpgsqlTypes.NpgsqlDbType.Varchar, (object)pw.WebPage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("_pw_photo", NpgsqlTypes.NpgsqlDbType.Bytea, (object)randomBytes ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                conn.Close();
                situation= "Başarılı";
                
                //}

            }
            catch (Exception e)
            {
                String hata = e.Message;
                situation = "Hata";
                
            }
            return situation;
        }
    }
}
