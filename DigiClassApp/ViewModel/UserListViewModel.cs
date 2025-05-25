using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigiClass.Interface;
using DigiClass.Models;
using DigiClass.Service;
using DigiClass.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DigiClass.ViewModel
{
    public partial class UserListViewModel:ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<UsersModel> _items;
        [ObservableProperty]
        private string _filtro = "";

        [ObservableProperty]
        private int _currentPage = 1;

        private int _pageItems = 5;

        private readonly IProductProvider<UsersModel> _productService;
        private List<UsersModel> _users;

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

            _users = await _productService.GetAllUsers(mainWindow.GetToken());
            Items = new ObservableCollection<UsersModel>();
            UpdatePage(_users);
        }

        private void UpdatePage(IEnumerable<UsersModel> cursos)
        {

            var itemsPage = cursos.ToList().Skip((CurrentPage - 1) * _pageItems).Take(_pageItems);
            Items.Clear();
            foreach (var planeta in itemsPage)
            {
                Items.Add(planeta);
            }
        }

        [RelayCommand]
        private void Filter()
        {
            IEnumerable<UsersModel> filter_items= _users.Where(e => e.name.Contains(Filtro));
            UpdatePage(filter_items);
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

        [RelayCommand]
        private void Next()
        {
            if (_users.ToList().Skip((CurrentPage) * _pageItems).Take(_pageItems).Count() != 0)
            {
                CurrentPage++;
                UpdatePage(_users);
            }

        }

        [RelayCommand]
        private void Previous()
        {
            if (CurrentPage - 1 >= 1)
            {
                CurrentPage--;
                UpdatePage(_users);
            }

        }

    }
}
