using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Parti_Uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult Main()
        {
            return View(Gets_FromDB_Class.getNews_withFilter(null,null,""));
        }

        [HttpGet]
        public JsonResult GetNewsDetails(int id)
        {
            NewsAdditionModel _nam = null;
            List<NewsAdditionModel> _nams = Gets_FromDB_Class.getNews_withFilter(null,null,"");
            foreach (var nam in _nams)
            {
                if (nam.Id == id)
                {
                    _nam = new NewsAdditionModel()
                    {
                        Id = nam.Id,
                        Photo = null,  //null olmayınca hata veriyor
                        Head = nam.Head,
                        Content = nam.Content,
                        Date = nam.Date
                    };
                }
            }
            return Json(_nam);
        }

    }
    }
