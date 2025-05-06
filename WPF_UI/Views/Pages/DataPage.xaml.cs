// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Demo.Mvvm.DTO;
using Wpf.Ui.Demo.Mvvm.Interfaces;
using Wpf.Ui.Demo.Mvvm.ViewModels;

namespace Wpf.Ui.Demo.Mvvm.Views.Pages;

/// <summary>
/// Interaction logic for DataView.xaml
/// </summary>
public partial class DataPage : INavigableView<ViewModels.DataViewModel>
{
    public ViewModels.DataViewModel ViewModel { get; }
    private readonly ICharacterProvider<CharacterDTO> _characterService;
    private MainWindowViewModel _mainWindowViewModel;
    private List<CharacterDTO> _characters;

    public DataPage(ViewModels.DataViewModel viewModel, ICharacterProvider<CharacterDTO> provider, MainWindowViewModel parent)
    {
        ViewModel = viewModel;
        DataContext = viewModel;
        InitializeComponent();
        Loaded += Page_Loaded;
        _characterService = provider;
        _mainWindowViewModel = parent;
        
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _characters= new List<CharacterDTO>();

        foreach (var character in await _characterService.Get(_mainWindowViewModel.token))
        {
            CharacterDTO newCharacter = new CharacterDTO()
            {
                id = character.id,
                name = character.name,
                damage = character.damage,
                createdDate = character.createdDate,
                Class = character.Class
            };
            _characters.Add(newCharacter);


        }

        this.ViewModel.OnNavigatedFromAsync();
    }

   

   

    /*private void FocusLost(object sender, RoutedEventArgs e)
    {
        List<Task<bool>> updateCharacters = new List<Task<bool>>();

        for (int i = 0; i < ViewModel.Characters.Count; i++)
        {
            CharacterDTO item = _characters.Find(x => x.id == ViewModel.Characters[i].id);
            if (!ViewModel.Characters[i].Equals(item))
            {
                int index=_characters.FindIndex(x => x.id == item.id);
                
                updateCharacters.Add(_characterService.PathCharacter(Utils.Constantes.POST_CHARACTER + $"/{ViewModel.Characters[i].id}", ViewModel.Characters[i], _mainWindowViewModel.token));
                _characters[index] = ViewModel.Characters[i];
            }

        }

    }*/

    
}
