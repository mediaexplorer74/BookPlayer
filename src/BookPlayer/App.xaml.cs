using BookPlayer.Interfaces;
using BookPlayer.Services;
using BookPlayer.ViewModels;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MediaManager;
using Xamarin.Forms;

namespace BookPlayer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            CrossMediaManager.Current.Init();
            
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<BookService>().As(typeof(IBookService)).SingleInstance();
            containerBuilder.RegisterType<PlayerService>().As(typeof(IPlayerService)).SingleInstance();
            containerBuilder.RegisterType<DataService>().As(typeof(IDataService)).SingleInstance();
            containerBuilder.RegisterType<OptionService>().As(typeof(IOptionService)).SingleInstance();
            containerBuilder.RegisterType<FileHandlingService>().As(typeof(IFileHandlingService)).SingleInstance();
            containerBuilder.RegisterType<PlayerViewModel>().As(typeof(PlayerViewModel)).SingleInstance();
            containerBuilder.RegisterType<AboutViewModel>().As(typeof(AboutViewModel)).SingleInstance();
            containerBuilder.RegisterType<BookshelfViewModel>().As(typeof(BookshelfViewModel)).SingleInstance();
            containerBuilder.RegisterType<SettingsViewModel>().As(typeof(SettingsViewModel)).SingleInstance();

            var container = containerBuilder.Build();            

            var asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
                        

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
