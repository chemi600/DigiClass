using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class CursoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Titulo { get; set; }

        public string Descripcion {  get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public string IdProfesor { get; set; }

        public AppUser Profesor { get; set; }

        public List<AppUser> Participantes { get; set; }




    }
}
