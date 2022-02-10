using BizimMarket.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket.Areas.Admin.Models
{
    public class YeniUrunViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "Kategori alanı zorunludur.")]
        public int? KategoriId { get; set; }

        [GecerliResim(MaksimumDosyaBoyutuMB = 2)]
        public IFormFile Resim { get; set; }
    }
}

// veritabanına resmi kaydetmiyoruz bu yüzden asıl urun classında iformfile ile yazmadık.