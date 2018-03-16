using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;
using Ninject;

namespace PeopleViewer
{
    public partial class App : Application
    {
        IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            _container.Bind<IPersonRepository>().To<ServiceRepository>().InSingletonScope();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = _container.Get<PeopleViewerWindow>();
            Application.Current.MainWindow.Title = "DI with Ninject - People Viewer";
        }
    }
}
