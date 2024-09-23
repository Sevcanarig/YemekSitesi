using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YemekTarifiApp.Models;

namespace YemekTarifiApp.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

}
