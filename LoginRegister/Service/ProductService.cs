using InfoManager.Helpers;
using InfoManager.Interface;
using InfoManager.Models;
using InfoManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoManager.Service
{
    public class ProductService<T> : IProductProvider<T> where T : class
    {
        public async Task<List<T>> GetAll(string token)
        {

            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.GetAsync(Constants.CURSOS);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(dataget);
            }

        }

        public async Task<List<T>> GetAllUsers(string token)
        {

            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.GetAsync(Constants.CURSOS);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(dataget);
            }

        }

        public async Task<List<T>> GetAllParticipantes(int cursoId,string token)
        {

            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.GetAsync(Constants.PARTICIPANTES+"/"+cursoId.ToString());
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(dataget);
            }

        }

        public async Task<T> Get(int id,string token)
        {

            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.GetAsync(Constants.PRODUCT_PATH+"/"+id.ToString());
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }

        }

        public async Task<bool> Delete(int cursoId, string token)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.DeleteAsync(Constants.CURSOS + "/" + cursoId.ToString());
                string dataget = await datos.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<T>(dataget);
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

        public async Task<bool> Delete(string userId, string token)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.DeleteAsync(Constants.CURSOS + "/" + userId);
                string dataget = await datos.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<T>(dataget);
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

        public async Task<bool> DeleteParticipante(string userId, string token, int cursoId)
        {
            using HttpClient httpClient = new HttpClient();
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                HttpResponseMessage datos = await httpClient.DeleteAsync(Constants.CURSOS + "/Participantes/"+cursoId+"&&" + userId);
                string dataget = await datos.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<T>(dataget);
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
