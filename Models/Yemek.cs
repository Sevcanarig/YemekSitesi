using System;
using System.Collections.Generic;

namespace YemekTarifiApp.Models
{
    public class Yemek
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? ResimUrl { get; set; }
        public string? Yapilis { get; set; }

        // Yemek-Malzeme ilişkisi
        public List<YemekMalzeme> YemekMalzemeler { get; set; }

        // Constructor
        public Yemek()
        {
            YemekMalzemeler = new List<YemekMalzeme>();  // 'YemekMalzeme' listesi başlatılıyor.
        }
    }
}
