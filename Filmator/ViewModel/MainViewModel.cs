using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Filmator.Model.Entities;
using Filmator.Model.Provider;
using GalaSoft.MvvmLight;
using Filmator.Model;
using GalaSoft.MvvmLight.Command;

namespace Filmator.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {
        private readonly IDataService _dataService;
        public MovieRemoteProvider ApiProvider { get; set;}
        public int Page { get; set; }

        #region Commands
        public ICommand GetPopularMoviesCommand { get; set; }
        public RelayCommand<object> CloseWindowCommand { get; set; }
        public RelayCommand<object> ToggleMaximizedWindowStateCommand { get; set; }
        public RelayCommand<object> SetMinimizeWindowStateCommand { get; set; }
        #endregion

        #region Actions
        private void GetPopularMoviesAction() {
            MoviesStored = new ObservableCollection<MovieStored>(ApiProvider.GetMostPopular(Page));
        }

        private void CloseWindowAction(object args) {
            var wnd = args as Window;
            if (wnd != null)
                wnd.Close();
        }

        private void ToggleMaximizedWindowStateAction(object args) {
            var wnd = args as Window;
            if (wnd != null)
                wnd.WindowState = wnd.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void SetMinimizeWindowStateAction(object args) {
            var wnd = args as Window;
            if (wnd != null)
                wnd.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Properties

        public const string MoviesStoredPropertyName = "MoviesStored";
        private ObservableCollection<MovieStored> _moviesStored;
        public ObservableCollection<MovieStored> MoviesStored {
            get { return _moviesStored; }
            set { _moviesStored = value; RaisePropertyChanged(MoviesStoredPropertyName); }
        }

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService) {
            ApiProvider = new MovieRemoteProvider();
            GetPopularMoviesCommand = new RelayCommand(GetPopularMoviesAction);
            CloseWindowCommand = new RelayCommand<object>(CloseWindowAction);
            ToggleMaximizedWindowStateCommand = new RelayCommand<object>(ToggleMaximizedWindowStateAction);
            SetMinimizeWindowStateCommand = new RelayCommand<object>(SetMinimizeWindowStateAction);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}