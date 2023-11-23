using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public class NewsAdditionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Fotoğraf alanı boş bırakılamaz.")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Haber başlığı boş bırakılamaz.")]
        public string Head { get; set; }

        [Required(ErrorMessage = "Haber içeriği boş bırakılamaz.")]
        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
