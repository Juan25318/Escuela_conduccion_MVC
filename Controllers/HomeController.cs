using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Seguimiento3.Models;
using MVC_Seguimiento3.Services;

namespace MVC_Seguimiento3.Controllers;

public class HomeController : Controller
{
    private readonly EscuelaService _escuela;

   
    public HomeController(EscuelaService escuela)
    {
        _escuela = escuela;
    }

   
    public IActionResult Index()
    {
        return View(_escuela.Cursos);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}