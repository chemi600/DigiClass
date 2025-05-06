using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Demo.Mvvm.DTO;

namespace Wpf.Ui.Demo.Mvvm.Interfaces
{
    public interface ILoginProvider<T> where T : class
    {
        Task<T> PostLogin(LoginDTO user);
    }
}
