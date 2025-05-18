using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InfoManager.Interface;
using InfoManager.Models;
using InfoManager.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InfoManager.ViewModel
{
    public partial class HomeViewModel: ViewModelBase
    {
        private readonly ILoginProvider<UserDTO> _loginService;

        [ObservableProperty]
        private string _name="ra@gmail.com";

        [ObservableProperty]
        private string _password="Abc123??";

        public HomeViewModel(ILoginProvider<UserDTO> registro)
        {
            _loginService = registro;
        }

        [RelayCommand]
        private async void Login()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Email = Name,
                Password = Password,
            };
            try
            {
                UserDTO user = await _loginService.PostLogin(loginDTO);

                if (!user.IsSuccess || user.Result.role != "admin")
                {
                    MessageBox.Show("Usuario o contraseñas incorrectos");
                } else
                {
                    MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
                    mainWindow.SetToken(user.Result.Token);
                    mainWindow.SelectViewModelCommand.Execute(App.Current.Services.GetService<DashboardViewModel>());
                }
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Usuario o contraseñas incorrectos");
            }
            


        }

        [RelayCommand]
        private async void Register()
        {

            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
           
            mainWindow.SelectedViewModel = App.Current.Services.GetService<MainViewModel>().RegisterView;
        }

        /*[RelayCommand]
        private void Nueva() 
        {
            var dialog = new SecondWindow();
            dialog.ShowDialog();
        }*/
    }
}
