using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket.Attributes
{
    public class GecerliResimAttribute : ValidationAttribute
    {
        public int MaksimumDosyaBoyutuMB { get; set; } = 1;

        public override bool IsValid(object value)
        {
            if (value == null) 
                return true;

            IFormFile resim = (IFormFile)value;

            if (resim.ContentType.StartsWith("image/"))
            {
                ErrorMessage = "Geçersiz resim dosyası."; 
                return false;
            }
            else if (resim.Length > MaksimumDosyaBoyutuMB * 1024 * 1024)
            {
                ErrorMessage = $"Resim dosyası {MaksimumDosyaBoyutuMB}MB'den büyük olamaz";
                return false;
            }

            return base.IsValid(value);
        }
    }
}
