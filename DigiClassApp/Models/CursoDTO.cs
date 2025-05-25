using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiClass.Models
{
    public class CursoDTO
    {
        public int id { get; set; }
        public string idProfesor { get; set; }
        public DateTime createdDate { get; set; }

        public string titulo { get; set; }

        public string descripcion { get; set; }

        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        public string nombreProfesor { get; set; }



    }
}
