using System.ComponentModel.DataAnnotations;
using YemekTarifiApp.Models;

public class Malzeme
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Malzeme adı gereklidir.")]
    public string Ad { get; set; } = string.Empty;

    public string? Birim { get; set; }

    public List<YemekMalzeme> YemekMalzemeler { get; set; } = new List<YemekMalzeme>();
}

