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
        private string _filtro="";

        [ObservableProperty]
        private int _currentPage = 1;

        private int _pageItems = 5;

        private readonly IProductProvider<CursoDTO> _productService;

        private ProductViewModel _selectedViewModel;
        private readonly IStringUtils _stringUtils;
        private List<CursoDTO> _products;

        [ObservableProperty]
        private string _mostrar="Hidden";
        [ObservableProperty]
        private string _mostrarButtonCerrar = "Hidden";

        [ObservableProperty]
        private string _mostrarButtonRecarga = "Visible";


        public ProductListViewModel(IProductProvider<CursoDTO> productService, ProductViewModel productView, IStringUtils stringUtils)
        {
            _productService = productService;
            _selectedViewModel = productView;
            _stringUtils = stringUtils;
        }
        public ProductViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public override async Task LoadAsync()
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();

            _products = await _productService.GetAll(mainWindow.GetToken());
            Items = new ObservableCollection<CursoDTO>();
            CurrentPage = 1;
            UpdatePage(_products);
        }

        private void UpdatePage(IEnumerable<CursoDTO> cursos)
        {

            var itemsPage = cursos.ToList().Skip((CurrentPage - 1) * _pageItems).Take(_pageItems);
            Items.Clear();
            foreach (var planeta in itemsPage)
            {
                Items.Add(planeta);
            }
        }

        /*[RelayCommand]
        private async Task SelectViewModel(object? parameter)
        {
            MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
            _productView.SetId(_stringUtils.ConvertToInteger(parameter?.ToString() ?? string.Empty) ?? int.MinValue);
            mainWindow.SelectViewModelCommand.Execute(_productView);

        }*/

        [RelayCommand]
        private void Filter()
        {
            IEnumerable<CursoDTO> filter_items = _products.Where(e => e.titulo.Contains(Filtro));
            UpdatePage(filter_items);
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

        [RelayCommand]
        private async void MostrarPanel(CursoDTO curso)
        {

            _selectedViewModel.SetId(curso.id);
            await _selectedViewModel.LoadAsync();
            Mostrar = "Visible";
            MostrarButtonCerrar = "Visible";
            MostrarButtonRecarga = "Hidden";
        }

        [RelayCommand]
        public void Cerrar()
        {
            
            Mostrar = "Hidden";
            MostrarButtonCerrar = "Hidden";
            MostrarButtonRecarga = "Visible";

        }

        [RelayCommand]
        private void Next()
        {
            if (_products.ToList().Skip((CurrentPage) * _pageItems).Take(_pageItems).Count() != 0)
            {
                CurrentPage++;
                UpdatePage(_products);
            }

        }

        [RelayCommand]
        private void Previous()
        {
            if (CurrentPage - 1 >= 1)
            {
                CurrentPage--;
                UpdatePage(_products);
            }

        }


    }
}
