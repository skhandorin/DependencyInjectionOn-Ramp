
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PersonRepository.Interface;
using PersonRepository.Service;
using PeopleViewer.Presentation;
using PersonRepository.CSV;
using PersonRepository.SQL;

namespace PeopleViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Application.Current.MainWindow.Title = "Loose Coupling - People Viewer";
            Application.Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            IPersonRepository repository = new SQLRepository(); ;
            var viewModel = new PeopleViewerViewModel(repository);
            Application.Current.MainWindow = new PeopleViewerWindow(viewModel);
        }
    }
}
