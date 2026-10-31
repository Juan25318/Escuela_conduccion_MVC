namespace MVC_Seguimiento3.Models;

public class ListadoInscripciones
{
    public List<Inscripcion> Inscripciones { get; set; } = new();
    public IReadOnlyList<Curso> Cursos { get; set; } = new List<Curso>();
    public string? CursoSeleccionado { get; set; }

    public decimal TotalRecaudado => Inscripciones.Sum(i => i.CalcularTotal());
}