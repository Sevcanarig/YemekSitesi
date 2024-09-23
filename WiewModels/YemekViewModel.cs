namespace YemekTarifiApp.ViewModels
{
    public class YemekViewModel
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Yapilis { get; set; }
        public string? ResimUrl { get; set; }
        public IFormFile? ResimDosya { get; set; }
        public List<MalzemeViewModel> Malzemeler { get; set; } = new List<MalzemeViewModel>();
        public List<MalzemeViewModel> MalzemeEklendi { get; set; } = new List<MalzemeViewModel>();
        public List<int> SelectedMalzemeler { get; set; } = new List<int>();
    }
}
