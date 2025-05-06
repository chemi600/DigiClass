using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InfoManager.Interface;
using InfoManager.Models;
using InfoManager.Service;
using InfoManager.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InfoManager.ViewModel
{
    public partial class UserListViewModel:ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<UsersModel> _items;
        [ObservableProperty]
        private string _filtro;

        private readonly IProductProvider<UsersModel> _productService;

        private readonly ProductViewModel _productView;
        private readonly IStringUtils _stringUtils;


        public UserListViewModel(IProductProvider<UsersModel> productService, ProductViewModel productView, IStringUtils stringUtils)
        {
            _productService = productService;
            _productView = productView;
            _stringUtils = stringUtils;
        }

        public override async Task LoadAsync()
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

            List<UsersModel> _users = await _productService.GetAllUsers(mainWindow.GetToken());
            Items = new ObservableCollection<UsersModel>();
            foreach (var planeta in _users)
            {
                Items.Add(planeta);
            }
        }

        

        [RelayCommand]
        private void Filter()
        {
            IEnumerable<UsersModel> filter_items= Items.Where(e => e.name.Contains(Filtro));
            Items.Clear();
            foreach (UsersModel item in filter_items)
            {
                Items.Add(item);
            }
        }

        [RelayCommand]
        private async void Recargar()
        {
            await LoadAsync();
        }

        [RelayCommand]
        private async void Delete(UsersModel user)
        {
            if (user != null)
            {
                MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

                if(await _productService.Delete(user.id, mainWindow.GetToken()))
                    Items.Remove(user);

            }
        }

    }
}
