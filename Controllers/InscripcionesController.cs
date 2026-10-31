using Microsoft.AspNetCore.Mvc;
using MVC_Seguimiento3.Models;
using MVC_Seguimiento3.Services;

namespace MVC_Seguimiento3.Controllers;

public class InscripcionesController : Controller
{
    private readonly EscuelaService _escuela;

    public InscripcionesController(EscuelaService escuela)
    {
        _escuela = escuela;
    }

    
    [HttpGet]
    public IActionResult Index(string? cursoId)
    {
        var modelo = new ListadoInscripciones
        {
            Inscripciones = _escuela.ObtenerInscripciones(cursoId),
            Cursos = _escuela.Cursos,
            CursoSeleccionado = cursoId
        };

        return View(modelo);
    }

    
    [HttpGet]
    public IActionResult Create()
    {
        CargarCursos();
        return View(new Inscripcion());
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Inscripcion inscripcion)
    {
        inscripcion.Nombre = inscripcion.Nombre?.Trim() ?? "";
        inscripcion.Correo = inscripcion.Correo?.Trim() ?? "";
        inscripcion.Telefono = inscripcion.Telefono?.Trim() ?? "";

    
        var curso = _escuela.BuscarCurso(inscripcion.CursoId);

        if (curso == null)
        {
            ModelState.AddModelError(nameof(Inscripcion.CursoId), "El curso seleccionado no existe.");
        }
        else if (_escuela.ExisteInscripcion(inscripcion.Correo, curso.Id))
        {
            ModelState.AddModelError(nameof(Inscripcion.Correo), "Ya existe una inscripción con ese correo para este curso.");
        }

       
        if (!ModelState.IsValid)
        {
            CargarCursos();
            return View(inscripcion);
        }

        _escuela.Registrar(inscripcion, curso!);

        
        TempData["Mensaje"] = $"¡{inscripcion.Nombre} quedó inscrito en el {curso!.Nombre}!";
        TempData["Tipo"] = "success";

        
        return RedirectToAction(nameof(Index));
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (_escuela.Eliminar(id))
        {
            TempData["Mensaje"] = "La inscripción fue eliminada.";
            TempData["Tipo"] = "warning";
        }
        else
        {
            TempData["Mensaje"] = "No se encontró la inscripción.";
            TempData["Tipo"] = "danger";
        }

        return RedirectToAction(nameof(Index));
    }

   
    private void CargarCursos()
    {
        ViewBag.Cursos = _escuela.Cursos;
    }
}