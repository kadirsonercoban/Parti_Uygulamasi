using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Parti_Uygulamasi.Models
{
    public static class ConvertJobs
    {
        public static byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static IFormFile ConvertBytesToIFormFile(byte[] fileBytes)
        {
            MemoryStream memoryStream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(memoryStream, 0, fileBytes.Length, null, null);
            return file;
        }

        public static byte[] ConvertObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            string json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
