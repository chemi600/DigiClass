using DigiClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiClass.Interface
{
    public interface IProductProvider<T> where T : class
    {
        Task<List<T>> GetAll(string token);
        Task<T> Get(int id,string token);
        Task<bool> Delete(int cursoId, string token);
        Task<List<T>> GetAllUsers(string token);
        Task<bool> Delete(string userId, string token);
        Task<List<T>> GetAllParticipantes(int cursoId, string token);
        Task<bool> DeleteParticipante(string userId, string token, int cursoId);


    }
}
