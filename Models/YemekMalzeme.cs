using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YemekTarifiApp.Models
{
    public class YemekMalzeme
    {
    public int YemekId { get; set; }
    public Yemek? Yemek { get; set; }
    public int MalzemeId { get; set; }
    public Malzeme? Malzeme{ get; set; }
    public string? Miktar { get; set; }

    }
}