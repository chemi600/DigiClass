using RestAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.CursoDTO
{
    public class CreateCurso
        {
            [Required(ErrorMessage = "Field required: Titulo")]
            public string Titulo { get; set; }
            [Required(ErrorMessage = "Field required: Descripcion")]
            public string Descripcion { get; set; }
            [Required(ErrorMessage = "Field required: FechaInicio")]
            public DateTime FechaInicio { get; set; }
            [Required(ErrorMessage = "Field required: FechaFin")]
            public DateTime FechaFin { get; set; }
            

        }
}
