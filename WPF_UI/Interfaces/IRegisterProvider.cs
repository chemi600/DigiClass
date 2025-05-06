using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Ui.Demo.Mvvm.Interfaces
{
    public interface IRegisterProvider<T> where T : class
    {
        Task<bool> PostUser(T user);
    }
}
