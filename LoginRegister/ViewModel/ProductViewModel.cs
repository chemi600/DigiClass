using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InfoManager.Interface;
using InfoManager.Models;
using InfoManager.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoManager.ViewModel
{
    public partial class ProductViewModel:ViewModelBase
    {
        

        private readonly IProductProvider<UsersModel> _productService;

        private int _cursoId;

        [ObservableProperty]
        private ObservableCollection<UsersModel> _items;
        

        public ProductViewModel(IProductProvider<UsersModel> productService)
        {
            _productService = productService;
           
        }
        public void SetId(int id)
        {
            _cursoId = id;
        }

        public override async Task LoadAsync()
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

            List<UsersModel> users = await _productService.GetAllParticipantes(_cursoId,mainWindow.GetToken());

            Items = new ObservableCollection<UsersModel>();
            foreach (var user in users)
            {
                Items.Add(user);
            }

        }
        [RelayCommand]
        public async void Quitar(UsersModel user)
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

           if(await _productService.DeleteParticipante(user.id, mainWindow.GetToken(), _cursoId))
            {
                Items.Remove(user);
            }
        }

        

       
    }
}
