using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Demo.Mvvm.Interfaces;
using WPF_UI.Utils;

namespace Wpf.Ui.Demo.Mvvm.Service
{
    public class RegisterService<T> : IRegisterProvider<T> where T : class
    {
        public async Task<bool> PostUser(T user)
        {
            return await HttpJsonClient<T>.PostBool(Utils.Constantes.POST_USER, user);
        }
    }
}
