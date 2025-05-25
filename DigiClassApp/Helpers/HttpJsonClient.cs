
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigiClass.Utils
{
    public static class HttpJsonClient<T>
    {
        public static async Task<T?> Get(string url)
        {
            using HttpClient httpClient = new HttpClient();
            {
                HttpResponseMessage datos = await httpClient.GetAsync(url);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
        }

        public static async Task<T?> Post(string url, T value) 
        {
            using HttpClient httpClient = new HttpClient();
            {
                StringContent jsoncontent = new(JsonSerializer.Serialize<T>(value), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(url, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
        }

        public static async Task<bool> PostBool(string url, T value)
        {
            using HttpClient httpClient = new HttpClient();
            {
                StringContent jsoncontent = new(JsonSerializer.Serialize<T>(value), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PostAsync(url, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return datos.IsSuccessStatusCode;
            }
        }

        public static async Task<T?> Put(string url, T value)
        {
            using HttpClient httpClient = new HttpClient();
            {
                StringContent jsoncontent = new(JsonSerializer.Serialize<T>(value), Encoding.UTF8, "application/json");
                HttpResponseMessage datos = await httpClient.PutAsync(url, jsoncontent);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
        }
    }
}
