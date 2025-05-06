// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;

public partial class MainWindowViewModel : ViewModel
{
    private bool _isInitialized = false;
    public string token;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _navigationItems = [];

    [ObservableProperty]
    private ObservableCollection<object> _navigationFooter = [];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [];

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "Demo"
    )]
    public MainWindowViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
        {
           
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "WPF UI - MVVM Demo";
        

        NavigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Cursos",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Usuarios",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.RegisterPage)
            },
        ];

        

        TrayMenuItems = [new() { Header = "Home", Tag = "tray_home" }];

        _isInitialized = true;
    }

    
   

    
}


