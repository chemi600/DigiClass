using DigiClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiClass.Interface
{
    public interface ILoginProvider<T> where T : class
    {
        Task<T> PostLogin(LoginDTO user);

        Task<bool> Register(UserRegistroDTO user);
    }
}
