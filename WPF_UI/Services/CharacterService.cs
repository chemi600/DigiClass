

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
    public class CharacterService<T> : ICharacterProvider<T> where T : class
    {
        public async Task<bool> PostCharacter(CreateCharacterDTO character,string token)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                StringContent jsoncontent = new(JsonSerializer.Serialize<CreateCharacterDTO>(character), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(Utils.Constantes.POST_CHARACTER, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return datos.IsSuccessStatusCode;
            }
  
        }

        public  async Task<List<T?>> Get(string token)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.GetAsync(Utils.Constantes.POST_CHARACTER);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(dataget);
            }
        }

        public async Task<bool> PathCharacter(string url,CharacterDTO character, string token)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                StringContent jsoncontent = new(JsonSerializer.Serialize<CharacterDTO>(character), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PatchAsync(url, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return datos.IsSuccessStatusCode;
            }

        }

    }
}
