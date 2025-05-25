using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigiClass.Interface;
using DigiClass.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DigiClass.ViewModel
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly ILoginProvider<UserDTO> _registerService;


        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _correo;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _passwordRepeat;

        public RegisterViewModel(ILoginProvider<UserDTO> registro) 
        {
            _registerService = registro;
        }

        [RelayCommand]
        private async void Registrarse()
        {

            if (Password != PasswordRepeat)
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }

            UserRegistroDTO user = new UserRegistroDTO()
            {
                Name = Name,
                UserName = Name,
                Email = Correo,
                Password = Password,
                Role = "admin"
            };
            if(await _registerService.Register(user))
            {
                MessageBox.Show("Registro exitoso");
                MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
                mainWindow.SelectedViewModel = App.Current.Services.GetService<MainViewModel>().HomeView;
            }
            else
            {
                MessageBox.Show("Registro incorrecto");
            }
        }

        [RelayCommand]
        private async void Back()
        {

            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
            

            mainWindow.SelectedViewModel = App.Current.Services.GetService<MainViewModel>().HomeView;
        }
    }
}
