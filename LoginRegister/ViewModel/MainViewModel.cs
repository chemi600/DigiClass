using CommunityToolkit.Mvvm.Input;


namespace InfoManager.ViewModel;

public partial class MainViewModel : ViewModelBase 
{
       private ViewModelBase? _selectedViewModel;

       private string _token;

    public MainViewModel(HomeViewModel home,RegisterViewModel register,ProductListViewModel productList)
        {
           
            
            
            _selectedViewModel = home;
            HomeView = home;
            RegisterView = register;
            ProductsList = productList;
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public RegisterViewModel RegisterView { get; }
        public HomeViewModel HomeView { get; }
        public ProductListViewModel ProductsList { get; }



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

    public void SetToken(string token) 
    {
        _token = token;
    }

    public string GetToken()
    {
        return _token;
    }
}
 

   

