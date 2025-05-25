using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace DigiClass.ViewModel;

public partial class DashboardViewModel : ViewModelBase 
{
       private ViewModelBase? _selectedViewModel;

       private string _token;

    public DashboardViewModel(ProductListViewModel productList,UserListViewModel users)
        {
           
            
            
            _selectedViewModel = productList;
           
            ProductsList = productList;
            UserListView = users;
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

       
        public ProductListViewModel ProductsList { get; }
        public UserListViewModel UserListView { get; }




    public override async Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        [RelayCommand]
        private async Task SelectViewModelAsync(object? parameter)
        {
            if (parameter is ViewModelBase viewModel)
            {
                SelectedViewModel = viewModel;
                await LoadAsync();
            }
        }

    [RelayCommand]
    private void Logout()
    {
        SetToken(string.Empty);
        MainViewModel mainWindow = App.Current.Services.GetService<MainViewModel>();
        

        mainWindow.SelectViewModelCommand.Execute(App.Current.Services.GetService<HomeViewModel>());
    }

    public void SetToken(string token) 
    {
        _token = token;
    }

    public string GetToken()
    {
        return _token;
    }
}
 

   

