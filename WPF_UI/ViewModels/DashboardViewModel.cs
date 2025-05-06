// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Demo.Mvvm.DTO;
using Wpf.Ui.Demo.Mvvm.Interfaces;

namespace Wpf.Ui.Demo.Mvvm.ViewModels;

public partial class DashboardViewModel : ViewModel
{
    [ObservableProperty]
    private int _damage = 10;
    [ObservableProperty]
    private string _nombre;
    [ObservableProperty]
    private string _clase;
    private MainWindowViewModel _mainWindowViewModel;
    private readonly ICharacterProvider<CreateCharacterDTO> _characterService;

    public DashboardViewModel(MainWindowViewModel parent, ICharacterProvider<CreateCharacterDTO> provider) 
    {
        _mainWindowViewModel = parent;
        _characterService = provider;
    }


    [RelayCommand]
    private async void enviar()
    {
        if(await _characterService.PostCharacter(new CreateCharacterDTO
        {
            Name = Nombre,
            Damage = Damage,
            Class = Clase
        }, _mainWindowViewModel.token)) 
        {
            MessageBox.Show("Personaje Añadido correctamente");
        }
        else 
        {
            MessageBox.Show("No se ha podido crear el personaje");
        }
        
    }
}
