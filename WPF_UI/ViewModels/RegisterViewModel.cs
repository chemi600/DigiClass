using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Navigation;
using Wpf.Ui.Demo.Mvvm.Models;
using Wpf.Ui.Demo.Mvvm.ViewModels;
using Wpf.Ui.Demo.Mvvm.Services;
using System.Windows.Controls;
using Wpf.Ui.Demo.Mvvm.Interfaces;
using Wpf.Ui.Demo.Mvvm.DTO;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;

    public partial class RegisterViewModel : ViewModel
    {
        private bool _isInitialized = false;
        private MainWindowViewModel _mainWindowViewModel;
        private readonly INavigationWindow _navigation;
        private readonly IRegisterProvider<RegisterDTO> _register;

    

    public RegisterViewModel(MainWindowViewModel parent,IRegisterProvider<RegisterDTO> service)
        {
            _mainWindowViewModel = parent;
            _navigation = (App.Services.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            _register = service;
        }

        [ObservableProperty]
        public string _txtUser;

        [ObservableProperty]
        public string _txtEmail;

        [ObservableProperty]
            public string _txtPassword;

    [ObservableProperty]
    public string _txtRepeatPassword;

    public override Task OnNavigatedFromAsync()
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
            return base.OnNavigatedFromAsync();
        }

        [RelayCommand]
        private async void clickRegister()
        {

        if (string.IsNullOrEmpty(TxtUser) || string.IsNullOrEmpty(TxtPassword)) 
        {
            MessageBox.Show("Los campos estan vacios");
            return;
        }

        if (TxtRepeatPassword != TxtPassword) 
        {
            MessageBox.Show("Las contraseñas no coinciden");
            return;
        }

        bool completed = await _register.PostUser(new RegisterDTO
        {
            UserName = TxtUser,
            UserEmail = TxtEmail,
            Name = TxtUser,
            Password = TxtPassword,
            Role = "user"
        });

        if (completed) 
        {
            MessageBox.Show("Registro completado");
            _navigation.Navigate(typeof(Views.Pages.LoginPage));
        }
        else 
        {
            MessageBox.Show("Registro incompleto");
            return;
        }

                
        


        }


        private void InitializeViewModel()
        {
            

            _isInitialized = true;
        }
    }

