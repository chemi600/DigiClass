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
    public partial class ProductListViewModel:ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<CursoDTO> _items;
        [ObservableProperty]
        private string _filtro;

        private readonly IProductProvider<CursoDTO> _productService;

        private readonly ProductViewModel _productView;
        private readonly IStringUtils _stringUtils;
        private List<CursoDTO> _products;


        public ProductListViewModel(IProductProvider<CursoDTO> productService, ProductViewModel productView, IStringUtils stringUtils)
        {
            _productService = productService;
            _productView = productView;
            _stringUtils = stringUtils;
        }

        public override async Task LoadAsync()
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

            _products = await _productService.GetAll(mainWindow.GetToken());
            Items = new ObservableCollection<CursoDTO>();
            foreach (var planeta in _products)
            {
                Items.Add(planeta);
            }
        }

        [RelayCommand]
        private async Task SelectViewModel(object? parameter)
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
            _productView.SetId(_stringUtils.ConvertToInteger(parameter?.ToString() ?? string.Empty) ?? int.MinValue);
            mainWindow.SelectViewModelCommand.Execute(_productView);

        }

        [RelayCommand]
        private void Filter()
        {
            IEnumerable<CursoDTO> filter_items= _products.Where(e => e.titulo.Contains(Filtro));
            Items.Clear();
            foreach (CursoDTO item in filter_items)
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
        private async void Delete(CursoDTO curso)
        {
            if (curso != null)
            {
                MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

                if(await _productService.Delete(curso.id, mainWindow.GetToken()))
                    Items.Remove(curso);

            }
        }

    }
}
