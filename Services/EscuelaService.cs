using MVC_Seguimiento3.Models;

namespace MVC_Seguimiento3.Services;

public class EscuelaService
{
    private readonly object _candado = new();
    private readonly List<Inscripcion> _inscripciones = new();
    private int _siguienteId = 1;

    public IReadOnlyList<Curso> Cursos { get; } = new List<Curso>
    {
        new() { Id = "automovil", Categoria = "Automóvil", Nombre = "Curso de Conducción de Automóvil",
                Duracion = "4 semanas", Horario = "Lunes a Viernes, 9:00 AM - 12:00 PM", Precio = 800 },
        new() { Id = "motocicleta", Categoria = "Motocicleta", Nombre = "Curso de Conducción de Motocicleta",
                Duracion = "4 semanas", Horario = "Lunes a Viernes, 1:00 PM - 4:00 PM", Precio = 400 },
        new() { Id = "camion", Categoria = "Camión", Nombre = "Curso de Conducción de Camiones",
                Duracion = "6 semanas", Horario = "Lunes a Viernes, 9:00 AM - 12:00 PM", Precio = 1200 },
        new() { Id = "defensiva_basica", Categoria = "Conducción Defensiva", Nombre = "Curso de Conducción Defensiva Básica",
                Duracion = "2 semanas", Horario = "Lunes a Viernes, 1:00 PM - 4:00 PM", Precio = 300 },
        new() { Id = "defensiva_avanzada", Categoria = "Conducción Defensiva", Nombre = "Curso de Conducción Defensiva Avanzada",
                Duracion = "3 semanas", Horario = "Lunes a Viernes, 9:00 AM - 12:00 PM", Precio = 500 }
    };

    public Curso? BuscarCurso(string? id)
    {
        return Cursos.FirstOrDefault(c => c.Id == id);
    }

    // Evita que la misma persona se inscriba dos veces en el mismo curso
    public bool ExisteInscripcion(string correo, string cursoId)
    {
        lock (_candado)
        {
            return _inscripciones.Any(i =>
                string.Equals(i.Correo, correo, StringComparison.OrdinalIgnoreCase) && i.CursoId == cursoId);
        }
    }

    public Inscripcion Registrar(Inscripcion inscripcion, Curso curso)
    {
        lock (_candado)
        {
            inscripcion.Id = _siguienteId++;
            inscripcion.FechaRegistro = DateTime.UtcNow;
            inscripcion.Curso = curso;
            _inscripciones.Add(inscripcion);
            return inscripcion;
        }
    }

    // filtro
    public List<Inscripcion> ObtenerInscripciones(string? cursoId = null)
    {
        lock (_candado)
        {
            IEnumerable<Inscripcion> consulta = _inscripciones;

            if (!string.IsNullOrEmpty(cursoId))
            {
                consulta = consulta.Where(i => i.CursoId == cursoId);
            }

            return consulta.ToList();
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado)
        {
            var inscripcion = _inscripciones.FirstOrDefault(i => i.Id == id);

            if (inscripcion == null)
            {
                return false;
            }

            _inscripciones.Remove(inscripcion);
            return true;
        }
    }
}