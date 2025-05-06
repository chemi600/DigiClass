

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WPF_UI.Utils;

using System.Text.Json;
using System.IO;
using Wpf.Ui.Demo.Mvvm.Interfaces;
using Wpf.Ui.Demo.Mvvm.DTO;
using System.Net.Http;
using System.Security.Policy;
namespace Wpf.Ui.Demo.Mvvm.Service
{
    public class LoginService<T> : ILoginProvider<T> where T : class
    {
        public async Task<T> PostLogin(LoginDTO user)
        {
            using HttpClient httpClient = new HttpClient();
            {
                
                StringContent jsoncontent = new(JsonSerializer.Serialize<LoginDTO>(user), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(Utils.Constantes.GET_USER, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
  
        }

    }
}
