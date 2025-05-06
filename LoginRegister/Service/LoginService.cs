using InfoManager.Interface;
using InfoManager.Models;
using InfoManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoManager.Service
{
    public class LoginService<T> : ILoginProvider<T> where T : class
    {
        public async Task<T> PostLogin(LoginDTO user)
        {
           
            using HttpClient httpClient = new HttpClient();
            {

                StringContent jsoncontent = new(JsonSerializer.Serialize<LoginDTO>(user), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(Helpers.Constants.LOGIN_PATH, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
            
        }

        public async Task<bool> Register(UserRegistroDTO user)
        {
            using HttpClient httpClient = new HttpClient();
            {

                StringContent jsoncontent = new(JsonSerializer.Serialize<UserRegistroDTO>(user), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(Helpers.Constants.REGISTER_PATH, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                //JsonSerializer.Deserialize<T>(dataget);
                if (datos.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}
