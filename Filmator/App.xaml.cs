using System.Windows;
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
