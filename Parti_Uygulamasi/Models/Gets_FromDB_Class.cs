using Microsoft.AspNetCore.Http;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public static class Gets_FromDB_Class
    {
        public static List<NewsAdditionModel> getNews_withFilter(DateTime? startDate,DateTime? endDate,string searchedText)
        {
            List<NewsAdditionModel> newsList = new List<NewsAdditionModel>();
            if (searchedText=="")
            {
                searchedText = null;
            }
            using (NpgsqlConnection conn = DatabaseManager.GetConnection())
            {   
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM getNews_withFilter(@startDate, @endDate, @searchedText)";
                        cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlParameter startDateParam = new NpgsqlParameter("@startDate", NpgsqlDbType.Date);
                        startDateParam.Value = (object)startDate ?? DBNull.Value;
                        cmd.Parameters.Add(startDateParam);

                        NpgsqlParameter endDateParam = new NpgsqlParameter("@endDate", NpgsqlDbType.Date);
                        endDateParam.Value = (object)endDate ?? DBNull.Value;
                        cmd.Parameters.Add(endDateParam);

                        NpgsqlParameter searchedTextParam = new NpgsqlParameter("@searchedText", NpgsqlDbType.Varchar);
                        searchedTextParam.Value = (object)searchedText ?? DBNull.Value;
                        cmd.Parameters.Add(searchedTextParam);


                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NewsAdditionModel news = new NewsAdditionModel
                                {
                                    Id = (int)reader["p_news_id"],
                                    Photo = ConvertJobs.ConvertBytesToIFormFile((byte[])reader["p_news_photo"]),
                                    Date = (DateTime)reader["p_news_date"],
                                    Head = (string)reader["p_news_head"],
                                    Content = (string)reader["p_news_content"]
                                };
                                newsList.Add(news);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    String hata = ex.Message;
                    Console.WriteLine(hata);
                }
            }
            return newsList;
        }
        public static List<PartyWorkerModel> getPartyWorkers()
        {
            List<PartyWorkerModel> partyworkersList = new List<PartyWorkerModel>();

            using (NpgsqlConnection conn = DatabaseManager.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM get_partyworkers_data()";
                        cmd.CommandType = System.Data.CommandType.Text;


                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = (int)reader["partyworkers_id"];
                                string firstName = (string)reader["partyworkers_name"];
                                string lastName = (string)reader["partyworkers_surname"];
                                object title_o = (object)reader["partyworkers_title"];
                                string title_s = title_o != DBNull.Value ? title_o.ToString() : null;
                                object city_o = (object)reader["partyworkers_city"];
                                string city_s = city_o != DBNull.Value ? city_o.ToString() : null;
                                object position_o = (object)reader["partyworkers_position"];
                                string position_s = position_o != DBNull.Value ? position_o.ToString() : null;
                                object intro_o = (object)reader["partyworkers_intro"];
                                string intro_s = intro_o != DBNull.Value ? intro_o.ToString() : null;
                                object phone_o = (object)reader["partyworkers_phone"];
                                string phone_s = phone_o != DBNull.Value ? phone_o.ToString() : null;
                                object email_o = (object)reader["partyworkers_email"];
                                string email_s = email_o != DBNull.Value ? email_o.ToString() : null;
                                object webpage_o = (object)reader["partyworkers_webpage"];
                                string webpage_s = webpage_o != DBNull.Value ? webpage_o.ToString() : null;

                                IFormFile photo_IFF = null;
                                object photo_o = reader["partyworkers_photo"];
                                if (photo_o != DBNull.Value)
                                {
                                    photo_IFF = ConvertJobs.ConvertBytesToIFormFile(ConvertJobs.ConvertObjectToByteArray(photo_o));
                                }


                                PartyWorkerModel partyworker = new PartyWorkerModel
                                {
                                    Id = id,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Title = title_s,
                                    City = city_s,
                                    Position = position_s,
                                    Introduction = intro_s,
                                    Phone = phone_s,
                                    Email = email_s,
                                    WebPage = webpage_s,
                                    Photo = photo_IFF
                                };
                                partyworkersList.Add(partyworker);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    String hata = ex.Message;
                   
                }
            }
            return partyworkersList;
        }
    }
}
