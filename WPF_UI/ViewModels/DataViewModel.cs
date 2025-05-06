// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Demo.Mvvm.DTO;
using Wpf.Ui.Demo.Mvvm.Interfaces;
using Wpf.Ui.Demo.Mvvm.Models;
using Wpf.Ui.Demo.Mvvm.Service;
using WPF_UI.Views;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;




public partial class DataViewModel : ViewModel
{
    [ObservableProperty]
    private int _currentPage = 1;

    private int _pageItems = 10;
   

    private bool _isInitialized = false;
    private MainWindowViewModel _mainWindowViewModel;
    private INavigationWindow _navigation;
    private readonly ICharacterProvider<CursoDTO> _characterService;

    [ObservableProperty]
    private ObservableCollection<CursoDTO> characters;

    public List<CursoDTO> charactersOriginal;


    public DataViewModel(MainWindowViewModel parent, ICharacterProvider<CursoDTO> provider)
    {
        _mainWindowViewModel = parent;
        _navigation = (App.Services.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
        _characterService = provider;
        

    }

   

   

    private void UpdatePage() 
    {
        
        var items= charactersOriginal.ToList().Skip((CurrentPage - 1)*_pageItems).Take(_pageItems);
        Characters.Clear();
        foreach (var item in items) 
        {
            Characters.Add(item);
        }
    }
    public override Task OnNavigatedFromAsync()
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
        return base.OnNavigatedFromAsync();
    }




    private async void InitializeViewModel()
    {


        await Recargar();

       

        _isInitialized = true;
    }

    [RelayCommand]
    private async Task<List<CursoDTO>> Recargar()
    {
        Characters ??= new ObservableCollection<CursoDTO>();
        Characters.Clear();
        charactersOriginal = new List<CursoDTO>();

        foreach (var character in await _characterService.Get(_mainWindowViewModel.token))
        {
            CursoDTO newCharacter = new CursoDTO()
            {
                id = character.id,
                titulo = character.titulo,
                descripcion = character.descripcion,
                createdDate = character.createdDate,
                fechaFin = character.fechaFin,
                fechaInicio = character.fechaInicio,
                idProfesor = character.idProfesor,
                nombreProfesor= character.nombreProfesor,
            };
            Characters.Add(newCharacter);
           

        }
        charactersOriginal=Characters.ToList();
        UpdatePage();
       return charactersOriginal;
       
    }

    [RelayCommand]
    private void GoToPage()
    {
        _navigation.Navigate(typeof(Views.Pages.DashboardPage));
    }

    [RelayCommand]
    private void Next()
    {
        if (charactersOriginal.ToList().Skip((CurrentPage) * _pageItems).Take(_pageItems).Count() != 0) 
        {
            CurrentPage++;
            UpdatePage();
        }
       
    }
    [RelayCommand]
    private void Eliminar_Click(object sender, RoutedEventArgs e)
    {
        Button boton = sender as Button;
        CursoDTO personaAEliminar = boton?.Tag as CursoDTO;

        if (personaAEliminar != null)
        {
            Characters.Remove(personaAEliminar);
        }
    }

    [RelayCommand]
    private void Previous()
    {
        if (CurrentPage-1>=1) 
        {
            CurrentPage--;
            UpdatePage();
        }
        
    }



}
