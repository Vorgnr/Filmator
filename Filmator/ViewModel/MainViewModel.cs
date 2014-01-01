using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Filmator.Model.Entities;
using Filmator.Model.Enums;
using Filmator.Model.Manager;
using Filmator.Model.Provider;
using Filmator.Model.Utils;
using GalaSoft.MvvmLight;
using Filmator.Model;
using GalaSoft.MvvmLight.Command;
using TMDbLib.Objects.General;

namespace Filmator.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {
        private readonly ISearchContainerService _dataService;
        public IMovieManager SearchManager { get; set; }

        #region Commands
        public RelayCommand<object> SetSelectedMovieCommand { get; set; } 
        public RelayCommand<string> SetSearchStateCommand { get; set; }
        public ICommand GetPopularMoviesCommand { get; set; }
        public ICommand ToggleSearchVisibilityCommand { get; set; }
        public ICommand ToggleBusyVisibilityCommand { get; set; }
        public RelayCommand IncrementPageCommand { get; private set; }
        public RelayCommand DecrementPageCommand { get; private set; }
        public RelayCommand<object> CloseWindowCommand { get; set; }
        public RelayCommand<object> ToggleMaximizedWindowStateCommand { get; set; }
        public RelayCommand<object> SetMinimizeWindowStateCommand { get; set; }
        public RelayCommand<int> AddToMyMoviesCommand{get;set;} 
        #endregion

        #region Actions
        #region View

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

        private void ToggleSearchVisibilityAction() {
            SearchVisibility = SearchVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void ToggleBusysVisibilityAction() {
            IsBusy = !IsBusy;
        }

        #endregion
        #region Query
        public void AddToMyMoviesAction(int id) {
            var movie = SearchManager.GetMovieStoredById(id);
            if (movie != null)
                SearchManager.Add(movie);
        }

        public bool CanAddToMyMovies(int id) {
            return id != 0;
        }

        public void IncrementPageAction() {
            Page++;
            SearchContainerOfMovieResult = SearchManager.GetSearchByState(SearchState, Page);
            IncrementPageCommand.RaiseCanExecuteChanged();
        }

        public void DecrementPageAction() {
            Page--;
            SearchContainerOfMovieResult = SearchManager.GetSearchByState(SearchState, Page);
            DecrementPageCommand.RaiseCanExecuteChanged();
        }

        public bool CanIncrementPage() {
            return Page < SearchContainerOfMovieResult.TotalPages;
        }

        public bool CanDecrementPage() {
            return Page > 1;
        }

        public void SetSelecteMovieAction(object args) {
            var movie = args as MovieResult;
            if (movie != null) SelectedMovie = SearchManager.GetMovieStoredById(movie.Id);
        }

        public void SetSearchStateAction(string descriptionState) {
            IsBusy = true;
            Page = 1;
            SearchState = EnumsHelper.GetEnumByDescription<SearchState>(descriptionState);
            SearchContainerOfMovieResult = SearchManager.GetSearchByState(SearchState);
            IsBusy = false;
        }
        #endregion
        #endregion

        #region Properties
        #region View
        public const string IsBusyPropertyName = "IsBusy";
        private bool _isBusy;
        public bool IsBusy {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(IsBusyPropertyName); }
        }

        public const string SearchVisibilityPropertyName = "SearchVisibility";
        private Visibility _searchVisibility;
        public Visibility SearchVisibility {
            get { return _searchVisibility; }
            set { _searchVisibility = value; RaisePropertyChanged(SearchVisibilityPropertyName); }
        }
        #endregion
        #region Query
        public const string PagePropertyName = "Page";
        private int _page;
        public int Page {
            get { return _page; }
            set { _page = value; RaisePropertyChanged(PagePropertyName); }
        }

        public const string SearchStatePropertyName = "SearchState";
        private SearchState _searchState;
        public SearchState SearchState {
            get { return _searchState; }
            set { _searchState = value; RaisePropertyChanged(SearchStatePropertyName); }
        }

        public const string SelectedMoviePropertyName = "SelectedMovie";
        private MovieStored _selectedMovie;
        public MovieStored SelectedMovie {
            get { return _selectedMovie; }
            set { _selectedMovie = value; RaisePropertyChanged(SelectedMoviePropertyName); }
        }

        public const string MoviesResultsPropertyName = "MoviesResults";
        private ObservableCollection<MovieResult> _moviesResults;
        public ObservableCollection<MovieResult> MoviesResults {
            get { return _moviesResults; }
            set { _moviesResults = value; RaisePropertyChanged(MoviesResultsPropertyName); }
        }

        public const string SearchContainerOfMovieResultPropertyName = "SearchContainerOfMovieResult";
        private SearchContainer<MovieResult> _searchContainerOfMovieResult;
        public SearchContainer<MovieResult> SearchContainerOfMovieResult {
            get { return _searchContainerOfMovieResult; }
            set { _searchContainerOfMovieResult = value; RaisePropertyChanged(SearchContainerOfMovieResultPropertyName); }
        }
        #endregion
        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ISearchContainerService dataService) {
            _dataService = dataService;
            _dataService.GetData(
            (item, error) => {
                if (error != null) {
                    // Report error here
                    return;
                }

                SearchContainerOfMovieResult = item;
            });
            Init();
            CloseWindowCommand = new RelayCommand<object>(CloseWindowAction);
            ToggleMaximizedWindowStateCommand = new RelayCommand<object>(ToggleMaximizedWindowStateAction);
            ToggleSearchVisibilityCommand =new RelayCommand(ToggleSearchVisibilityAction);
            SetMinimizeWindowStateCommand = new RelayCommand<object>(SetMinimizeWindowStateAction);
            SetSearchStateCommand = new RelayCommand<string>(SetSearchStateAction);
            SetSelectedMovieCommand = new RelayCommand<object>(SetSelecteMovieAction);
            IncrementPageCommand = new RelayCommand(IncrementPageAction, CanIncrementPage);
            DecrementPageCommand = new RelayCommand(DecrementPageAction, CanDecrementPage);
            AddToMyMoviesCommand = new RelayCommand<int>(AddToMyMoviesAction, CanAddToMyMovies);
        }

        private void Init() {
            SearchManager = new MovieManager();
            IsBusy = false;
            SearchVisibility = Visibility.Hidden;
            SearchState = SearchState.NowPlaying;
            SetSearchStateAction(SearchState.ToString());
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}