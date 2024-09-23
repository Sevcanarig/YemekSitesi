using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekTarifiApp.Data;
using YemekTarifiApp.Models;
using YemekTarifiApp.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YemekTarifiApp.Controllers
{
    public class YemekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YemekController(ApplicationDbContext context)
        {
            _context = context;
        }

        private List<MalzemeViewModel> GetMalzemeler()
        {
            return _context.Malzemeler.Select(m => new MalzemeViewModel
            {
                Id = m.Id,
                Ad = m.Ad
            }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var yemekler = await _context.Yemekler
                .Include(y => y.YemekMalzemeler)
                .ThenInclude(ym => ym.Malzeme)
                .ToListAsync();

            var yemekViewModelList = yemekler.Select(y => new YemekViewModel
            {
                Id = y.Id,
                Ad = y.Ad,
                Yapilis = y.Yapilis,
                ResimUrl = y.ResimUrl,
                Malzemeler = y.YemekMalzemeler
                    .Where(ym => ym.Malzeme != null)
                    .Select(ym => new MalzemeViewModel
                    {
                        Id = ym.MalzemeId,
                        Ad = ym.Malzeme?.Ad ?? "Bilinmiyor",
                        Miktar = ym.Miktar ?? "Bilinmiyor"
                    }).ToList()
            }).ToList();

            return View(yemekViewModelList);
        }

        public async Task<IActionResult> Detay(int? id)
        {
            if (id == null)
                return NotFound();

            var yemek = await _context.Yemekler
                .Include(y => y.YemekMalzemeler)
                .ThenInclude(ym => ym.Malzeme)
                .FirstOrDefaultAsync(y => y.Id == id);

            if (yemek == null)
                return NotFound();

            var malzemeler = yemek.YemekMalzemeler?
                .Select(ym => new MalzemeViewModel
                {
                    Id = ym.Malzeme?.Id ?? 0,
                    Ad = ym.Malzeme?.Ad ?? "Bilinmiyor",
                    Miktar = ym.Miktar ?? "Bilinmiyor"
                }).ToList() ?? new List<MalzemeViewModel>();

            ViewBag.Malzemeler = malzemeler;
            return View(yemek);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            var model = new YemekViewModel
            {
                Malzemeler = GetMalzemeler(),
                MalzemeEklendi = new List<MalzemeViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(YemekViewModel model)
        {
            if (ModelState.IsValid)
            {
                var yemek = new Yemek
                {
                    Ad = model.Ad,
                    Yapilis = model.Yapilis,
                    ResimUrl = model.ResimDosya != null ? await UploadImage(model.ResimDosya) : null,
                    YemekMalzemeler = model.MalzemeEklendi
                        .Select(m => new YemekMalzeme
                        {
                            MalzemeId = m.Id,
                            Miktar = m.Miktar
                        }).ToList()
                };

                var validMalzemeIds = _context.Malzemeler.Select(m => m.Id).ToList();
                if (yemek.YemekMalzemeler.Any(ym => !validMalzemeIds.Contains(ym.MalzemeId)))
                {
                    ModelState.AddModelError("", "Geçersiz malzeme id'si mevcut.");
                    model.Malzemeler = GetMalzemeler();
                    return View(model);
                }

                _context.Add(yemek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Malzemeler = GetMalzemeler();
            return View(model);
        }

        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
                return NotFound();

            var yemek = await _context.Yemekler
                .Include(y => y.YemekMalzemeler)
                .ThenInclude(ym => ym.Malzeme)
                .FirstOrDefaultAsync(y => y.Id == id);

            if (yemek == null)
                return NotFound();

            var model = new YemekViewModel
            {
                Id = yemek.Id,
                Ad = yemek.Ad,
                Yapilis = yemek.Yapilis,
                ResimUrl = yemek.ResimUrl,
                SelectedMalzemeler = yemek.YemekMalzemeler.Select(ym => ym.MalzemeId).ToList(),
                Malzemeler = GetMalzemeler(),
                MalzemeEklendi = yemek.YemekMalzemeler.Select(ym => new MalzemeViewModel
                {
                    Id = ym.MalzemeId,
                    Ad = ym.Malzeme?.Ad ?? "Bilinmiyor",
                    Miktar = ym.Miktar ?? "Bilinmiyor"
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, YemekViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingYemek = await _context.Yemekler
                        .Include(y => y.YemekMalzemeler)
                        .FirstOrDefaultAsync(y => y.Id == id);

                    if (existingYemek == null)
                        return NotFound();

                    existingYemek.Ad = model.Ad;
                    existingYemek.Yapilis = model.Yapilis;

                    if (model.ResimDosya != null)
                        existingYemek.ResimUrl = await UploadImage(model.ResimDosya);

                    existingYemek.YemekMalzemeler.Clear();

                    if (model.MalzemeEklendi != null)
                    {
                        var validMalzemeIds = _context.Malzemeler.Select(m => m.Id).ToList();
                        foreach (var malzeme in model.MalzemeEklendi)
                        {
                            if (validMalzemeIds.Contains(malzeme.Id))
                            {
                                existingYemek.YemekMalzemeler.Add(new YemekMalzeme
                                {
                                    MalzemeId = malzeme.Id,
                                    YemekId = id,
                                    Miktar = malzeme.Miktar
                                });
                            }
                        }
                    }

                    _context.Update(existingYemek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YemekExists(model.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            model.Malzemeler = GetMalzemeler();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var yemek = await _context.Yemekler.FirstOrDefaultAsync(m => m.Id == id);
            if (yemek == null)
                return NotFound();

            return View(yemek);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yemek = await _context.Yemekler.FindAsync(id);
            if (yemek != null)
            {
                _context.Yemekler.Remove(yemek);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool YemekExists(int id)
        {
            return _context.Yemekler.Any(e => e.Id == id);
        }

        private async Task<string> UploadImage(IFormFile resimDosya)
        {
            if (resimDosya == null || resimDosya.Length == 0)
                return null;

            var dosyaAdi = Path.GetFileNameWithoutExtension(resimDosya.FileName);
            var uzanti = Path.GetExtension(resimDosya.FileName);
            dosyaAdi = $"{dosyaAdi}_{DateTime.Now:yyMMddHHmmss}{uzanti}";
            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", dosyaAdi);

            using (var fileStream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await resimDosya.CopyToAsync(fileStream);
            }

            return $"/Img/{dosyaAdi}";
        }
        public async Task<IActionResult> RecipeList()
{
    var tarifler = await _context.Yemekler
        .Include(y => y.YemekMalzemeler)
        .ThenInclude(ym => ym.Malzeme)
        .ToListAsync();

    return View("Recipe", tarifler);
}

    }
}
