using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InfoManager.Helpers
{
    public static class Constants
    {
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        public const string BASE_URL = "http://localhost:5072/api/";
        public const string DICATADOR_URL = "Dicatador";
        public const string LOGIN_PATH = BASE_URL+"users/login";
        public const string REGISTER_PATH = BASE_URL+"users/register";
        public const string PRODUCT_PATH = BASE_URL + "Product";
        public const string CURSOS = BASE_URL+"Curso";
        public const string USERS = BASE_URL + "users";
        public const string PARTICIPANTES = USERS + "/Participantes";



    }
}
