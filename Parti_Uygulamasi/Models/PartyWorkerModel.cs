using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public class PartyWorkerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }//ünvan
        public string City { get; set; }//görev yeri
        public string Position { get; set; }//görevi
        public string Introduction { get; set; }//kişi tanıtım
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }
        public IFormFile Photo { get; set; }
    }
}
