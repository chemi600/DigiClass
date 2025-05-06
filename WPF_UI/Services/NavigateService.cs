using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Wpf.Ui.Demo.Mvvm.Services
{
    public class NavigateService(IServiceProvider serviceProvider)
    {
        public void NavigateTo(Type pageModel)
        {
            INavigationWindow navigationWindow = (serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            navigationWindow.Navigate(pageModel);
        }
    }
}
