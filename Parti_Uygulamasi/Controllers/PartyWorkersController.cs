using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Parti_Uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Parti_Uygulamasi.Controllers
{
    public class PartyWorkersController : Controller
    {
        public IActionResult Add_PWorkers()
        {
            return View();
        }

        public IActionResult List_PWorkers()
        {
            return View(Gets_FromDB_Class.getPartyWorkers());
        }

        [HttpPost]
        public IActionResult AddPartyWorker(PartyWorkerModel pw)
        {
            string situation = Add_ToDB.AddPartyWorker(pw);
            if (situation == "Başarılı")
            {
                ViewBag.Situation = "Başarılı";
            }
            else
            {
                ViewBag.Situation = "Hata";
            }     
            return RedirectToAction("Add_PWorkers", "PartyWorkers");
        }

        [HttpGet]
        public JsonResult GetPartyWorkerDetails(int id)
        {
            List<PartyWorkerModel> pworkers = Gets_FromDB_Class.getPartyWorkers();
            PartyWorkerModel pwm = null;
            foreach (var pw in pworkers)
            {
                if (pw.Id == id)
                {
                    pwm = new PartyWorkerModel()
                    {
                        Id = pw.Id,
                        FirstName=pw.FirstName,
                        LastName=pw.LastName,
                        Title=pw.Title,
                        City=pw.City,
                        Position=pw.Position,
                        Introduction=pw.Introduction,
                        Phone=pw.Phone,
                        Email=pw.Email,
                        WebPage=pw.WebPage,
                        Photo=null
                    };
                }
            }
            return Json(pwm); 
        }

        public IActionResult DeletePW(int personId)
        {
            string situation=Deletes_FromDB.DeletePW(personId);
            return RedirectToAction("List_PWorkers", "PartyWorkers");
        }

        [HttpPost]
        public string UpdatePW([FromBody] PartyWorkerModel pw)
        {
            string situation = Update_ToDB.UpdatePW(pw);
            return situation;
        }

    }
}
