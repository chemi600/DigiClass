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
using Wpf.Ui.Demo.Mvvm.Interfaces;
using Wpf.Ui.Demo.Mvvm.DTO;
using Wpf.Ui.Controls;
using System.Windows.Input;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;

    public partial class LoginViewModel : ViewModel
    {
        private bool _isInitialized = false;
        private MainWindowViewModel _mainWindowViewModel;
        private readonly INavigationWindow _navigation;
        private readonly ILoginProvider<UserLoginResponseDto> _loginService;

    

    public LoginViewModel(MainWindowViewModel parent,ILoginProvider<UserLoginResponseDto> service)
        {
            _mainWindowViewModel = parent;
            _navigation = (App.Services.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            _loginService = service;
            LogoutCommand = new RelayCommand(Logout);

    }

        [ObservableProperty]
        public string _txtUser;

        [ObservableProperty]
        public string _txtPassword;

    public override Task OnNavigatedFromAsync()
        {
            if (!_isInitialized)
            {
                
                InitializeViewModel();
            }
            return base.OnNavigatedFromAsync();
        }

        [RelayCommand]
        private async void clickLogin()
        {

        if (string.IsNullOrEmpty(TxtUser) || string.IsNullOrEmpty(TxtPassword)) 
        {
            System.Windows.MessageBox.Show("Campos incompletos");
        }

        UserLoginResponseDto log= await _loginService.PostLogin(new LoginDTO
        {
            email = TxtUser,
            password = TxtPassword
        });

        if (log.result != null)
        {
            _mainWindowViewModel.token = log.result.token;

            _mainWindowViewModel.NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Data",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Añadir Personaje",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            }
        ];
            _mainWindowViewModel.NavigationFooter =
                [
                    new NavigationViewItem()
                    {
                        Content = "Logout",
                        Icon = new SymbolIcon { Symbol = SymbolRegular.Power20 },
                        Command=LogoutCommand
                    }
                ];

            _navigation.Navigate(typeof(Views.Pages.DataPage));
        }

        
         


        }


        private void InitializeViewModel()
        {
            

            _isInitialized = true;
        }

        public ICommand LogoutCommand { get; }
        public void Logout() 
        {
            _mainWindowViewModel.NavigationFooter.Clear();
            _mainWindowViewModel.NavigationItems =
            [
            new NavigationViewItem()
            {
                Content = "Login",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.LoginPage)
            },
            new NavigationViewItem()
            {
                Content = "Registro",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.RegisterPage)
            },
        ];
            _navigation.Navigate(typeof(Views.Pages.LoginPage));


    }
}

