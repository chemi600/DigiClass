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
        

        private readonly IProductProvider<Product> _productService;

        private int _productId;

        [ObservableProperty]
        private string _name;

        public ProductViewModel(IProductProvider<Product> productService)
        {
            _productService = productService;
           
        }
        public void SetId(int id)
        {
            _productId = id;
        }

        public override async Task LoadAsync()
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

            Product planetas = await _productService.Get(_productId,mainWindow.GetToken());
            Name=planetas.Name;
           
        }

       
    }
}
