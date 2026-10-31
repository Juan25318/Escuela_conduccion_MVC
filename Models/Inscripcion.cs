using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVC_Seguimiento3.Models;

public class Inscripcion
{
    public const decimal PrecioClasesExtra = 100;
    public const decimal PrecioMaterial = 50;
    public const int EdadMinima = 16;

    [BindNever]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Escribe un correo válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "La edad es obligatoria.")]
    [Range(EdadMinima, 100, ErrorMessage = "La edad mínima para inscribirse es de 16 años.")]
    [Display(Name = "Edad")]
    public int? Edad { get; set; }

    [Required(ErrorMessage = "Selecciona un curso.")]
    [Display(Name = "Curso")]
    public string CursoId { get; set; } = "";

    [Display(Name = "Clases prácticas adicionales")]
    public bool ClasesExtra { get; set; }

    [Display(Name = "Material de estudio impreso")]
    public bool Material { get; set; }

    [BindNever]
    public DateTime FechaRegistro { get; set; }

    [BindNever]
    public Curso? Curso { get; set; }

    // Calcula el total segun el curso
    public decimal CalcularTotal()
    {
        if (Curso == null)
        {
            return 0;
        }

        var total = Curso.Precio;

        if (ClasesExtra)
        {
            total += PrecioClasesExtra;
        }

        if (Material)
        {
            total += PrecioMaterial;
        }

        return total;
    }
}