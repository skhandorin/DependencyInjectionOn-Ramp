using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;
using Microsoft.Practices.Unity;

namespace PeopleViewer
{
    public partial class App : Application
    {
        IUnityContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<IPersonRepository, SQLRepository>(new ContainerControlledLifetimeManager());
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = _container.Resolve<PeopleViewerWindow>();
            Application.Current.MainWindow.Title = "DI with Unity - People Viewer";
        }
    }
}
