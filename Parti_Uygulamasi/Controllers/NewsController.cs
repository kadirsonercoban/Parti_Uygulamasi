using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Parti_Uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult NewsP()
        {
            return View();
        }

        public IActionResult NewsD(int pageNumber = 1, DateTime? start_date = null, DateTime? end_date = null, string searchedText = "")
        {
            int pageSize = 5;
            List<NewsAdditionModel> news = Gets_FromDB_Class.getNews_withFilter(start_date,end_date,searchedText);
            int maxPageNumber = (int)Math.Ceiling((double)news.Count() / pageSize);
            int count = 0;
            int pages_start = (pageNumber - 1) * pageSize;
            List<NewsAdditionModel> pageNews = new List<NewsAdditionModel>();
            foreach (var _new in news)
            {
                count += 1;
                if(count>=pages_start && count < (pages_start + pageSize))
                {
                    pageNews.Add(_new);
                }
            }
            ViewBag.CurrentPageNumber = pageNumber;
            ViewBag.MaxPageNumber = maxPageNumber;
            return View(pageNews);
        }

        [HttpPost]
        public IActionResult AddNews(NewsAdditionModel newAdditionModel)
        {
            try
            {
                //if (newAdditionModel.Photo != null && newAdditionModel.Photo.Length > 0)
                //{
                    //byte[] photo = ConvertJobs.ConvertIFormFileToBytes(newAdditionModel.Photo);
                    NpgsqlConnection conn = DatabaseManager.GetConnection();
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert_new";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    Random random = new Random();
                    byte[] randomBytes = new byte[10];
                    random.NextBytes(randomBytes);

                    // Prosedüre parametreleri ekleyin
                    cmd.Parameters.AddWithValue("photo", NpgsqlTypes.NpgsqlDbType.Bytea, randomBytes);
                    cmd.Parameters.AddWithValue("head", NpgsqlTypes.NpgsqlDbType.Varchar, newAdditionModel.Head);
                    cmd.Parameters.AddWithValue("_content", NpgsqlTypes.NpgsqlDbType.Varchar, newAdditionModel.Content);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                   
                //}

            }
            catch (Exception e)
            {
                String hata = e.Message;
                
            }


            return RedirectToAction("NewsP", "News");
        }

        [HttpPost]
        public string DeleteNews([FromBody] int[] ids)
        {
            try
            {
                NpgsqlConnection conn = DatabaseManager.GetConnection();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete_news_by_ids";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Prosedüre parametreleri ekleyin
                cmd.Parameters.AddWithValue("news_ids", NpgsqlTypes.NpgsqlDbType.Integer | NpgsqlTypes.NpgsqlDbType.Array, ids);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                String hata = e.Message;
                Console.WriteLine(hata);
            }
            //return RedirectToAction("NewsD", "News");
            return "/News/NewsD";
        }

    }
}
