using System.Data.Entity;
using System.Windows;
using Filmator.Model;
using GalaSoft.MvvmLight.Threading;

namespace Filmator {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        static App() {
            //Database.SetInitializer<FilmatorContext>(new DropCreateDatabaseAlways<FilmatorContext>());
            DispatcherHelper.Initialize();
        }
    }
}
