using RestAPI.Models.DTOs;
using RestAPI.Models.Entity;

namespace RestAPI.Models.DTOs.CursoDTO
{
    public class CursoDTO : CreateCurso
    {
       public int Id { get; set; }
        public string IdProfesor { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string NombreProfesor { get; set; }


    }
}
