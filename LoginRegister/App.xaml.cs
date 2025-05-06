using InfoManager.ViewModel;
using InfoManager.Service;
using InfoManager.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using InfoManager.Models;
using InfoManager.View;


namespace InfoManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Current.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddSingleton<MainWindow>();


            //view viewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<ViewModelBase>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<ProductListViewModel>();
            services.AddTransient<ProductViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<UserListViewModel>();





            //Services
            services.AddSingleton<LoginDTO>();   
            services.AddSingleton(typeof(ILoginProvider<>), typeof(LoginService<>));
            services.AddSingleton(typeof(IProductProvider<>), typeof(ProductService<>));
            services.AddSingleton(typeof(IFileService<>), typeof(FileService<>));

            services.AddSingleton<IStringUtils, StringUtils>();

            return services.BuildServiceProvider();
        }
    }
}


