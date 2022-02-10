using BizimMarket.Areas.Admin.Models;
using BizimMarket.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace BizimMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunlerController : Controller
    {
        private readonly BizimMarketContext _db;
        private readonly IWebHostEnvironment _env;

        public UrunlerController(BizimMarketContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Urunler.Include(x => x.Kategori).ToList());
        }

        // get
        public IActionResult Yeni()
        {
            ViewBag.Kategoriler = _db.Kategoriler
                .Select(x => new SelectListItem(x.Ad, x.Id.ToString()))
                .ToList();
            return View();
        }

        // post
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(YeniUrunViewModel vm)     // Burası modem binding. Bir action, method da bir complex tip belirtildiğinde ürün vb gibi. Formdan gelen dataların action methodlardaki belirtilen türün propertylerine aktarılmasına denir. Ancak bu işlem esnasında propertylerin üzerindeki valuedating entribute larda dikkate alınır. Required, max value vb. Bu hata mesajı model state nesnesine eklenir.  Binding = bağlama
        {
            if (ModelState.IsValid)
            {
                #region Resim Kaydetme
                string dosyaAdi = null;
                if (vm.Resim != null)
                {
                    dosyaAdi = Guid.NewGuid() + Path.GetExtension(vm.Resim.FileName);
                    // dosyanın . dan sonraki kısmını alma!!
                    string kaydetmeYolu = Path.Combine(_env.WebRootPath , "img", "urunler", dosyaAdi);
                    // string kaydetmeYolu = _env.WebRootPath + @"\img\urunler\" + dosyaAdi;
                    using (var fs = new FileStream(kaydetmeYolu, FileMode.Create))
                    {
                        vm.Resim.CopyTo(fs);
                    } 
                }
                #endregion

                Urun urun = new Urun()
                {
                    Ad = vm.Ad,
                    Fiyat = vm.Fiyat.Value,
                    KategoriId = vm.KategoriId.Value,
                    ResimYolu = dosyaAdi
                };
                _db.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kategoriler = _db.Kategoriler
                .Select(x => new SelectListItem(x.Ad, x.Id.ToString()))
                .ToList();
            return View();
        }
    }
}


// CTRL + K + S             ** herhagi bir şey içersine alma(if, region vs.)