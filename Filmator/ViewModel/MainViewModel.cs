using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Filmator.Model.Enums;
using Filmator.Model.Manager;
using Filmator.Model.Utils;
using GalaSoft.MvvmLight;
using Filmator.Model;
using GalaSoft.MvvmLight.Command;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace Filmator.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {
        private readonly ISearchContainerService _dataService;
        public IMovieManager MovieManager { get; set; }
        public IGenreManager GenreManager { get; set; }

        #region Commands

        #region ToggleVisibility
        public ICommand ToggleBusyVisibilityCommand { get; set; }
        public ICommand ToggleSearchVisibilityCommand { get; set; }
        public ICommand ToggleMovieListVisibilityCommand { get; set; }
        public ICommand ToggleSelectedMovieVisibilityCommand { get; set; }
        public ICommand ToggleGenreListVisibilityCommand { get; set; }
        #endregion

        public RelayCommand<object> DragMoveCommand { get; set; } 
        public RelayCommand<object> SetSelectedMovieCommand { get; set; }
        public RelayCommand<object> SetSelectedGenreCommand { get; set; } 
        public RelayCommand<string> SetSearchStateCommand { get; set; }
        public ICommand GetPopularMoviesCommand { get; set; }
        public RelayCommand IncrementPageCommand { get; private set; }
        public RelayCommand DecrementPageCommand { get; private set; }
        public RelayCommand<object> CloseWindowCommand { get; set; }
        public RelayCommand<object> ToggleMaximizedWindowStateCommand { get; set; }
        public RelayCommand<object> SetMinimizeWindowStateCommand { get; set; }
        public RelayCommand<int> AddToMyMoviesCommand{get;set;} 
        #endregion

        #region Actions
        #region View

        #region Visibility
        private void ToggleSearchVisibilityAction() {
            SearchVisibility = SearchVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void ToggleBusysVisibilityAction() {
            IsBusy = !IsBusy;
        }

        private void ToggleMovieListVisibilityAction() {
            MovieListVisibility = MovieListVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void ToggleSelectedMovieVisibilityAction() {
            SelectedMovieVisibility = SelectedMovieVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void ToggleGenreListVisibilityAction() {
            GenreListVisibility = GenreListVisibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion

        private void DragMoveAction(object arg) {
            var wnd = arg as Window;
            if (wnd != null)
                wnd.DragMove();
        }

        private void CloseWindowAction(object arg) {
            var wnd = arg as Window;
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
        #region Query
        public void AddToMyMoviesAction(int id) {
            //var movie = MovieManager.GetMovieStoredById(id);
            //if (movie != null)
            //    //MovieManager.Add(movie);
        }

        public bool CanAddToMyMovies(int id) {
            return id != 0;
        }

        public void IncrementPageAction() {
            Page++;
            //SearchContainerOfMovieResult = SearchState == SearchState.Genre ? GenreManager.GetSearchContainerByGenreId(SelectedGenre.Id, Page) : MovieManager.GetSearchByState(SearchState, Page);
            IncrementPageCommand.RaiseCanExecuteChanged();
        }

        public void DecrementPageAction() {
            Page--;
            //SearchContainerOfMovieResult = SearchState == SearchState.Genre ? GenreManager.GetSearchContainerByGenreId(SelectedGenre.Id, Page) : MovieManager.GetSearchByState(SearchState, Page);
            DecrementPageCommand.RaiseCanExecuteChanged();
        }

        public bool CanIncrementPage() {
            return Page < SearchContainerOfMovieResult.TotalPages;
        }

        public bool CanDecrementPage() {
            return Page > 1;
        }

        public void SetSelecteMovieAction(object arg) {
            SelectedMovieVisibility = Visibility.Visible;
            GenreListVisibility = Visibility.Hidden;
            var movie = arg as MovieResult;
            //if (movie != null) SelectedMovie = MovieManager.GetMovieStoredById(movie.Id);
        }

        public void SetSearchStateAction(string descriptionState) {
            Page = 1;
            SearchState = EnumsHelper.GetEnumByDescription<SearchState>(descriptionState);
            //SearchContainerOfMovieResult = MovieManager.GetSearchByState(SearchState, Page, CustomSearch);
            MovieListVisibility = Visibility.Visible;
        }

        public void SetSelectedGenreAction(object arg) {
            Page = 1;
            var genre = arg as Genre;
            SearchState = SearchState.Genre;
            SelectedGenre = genre;
            if (genre != null) SearchContainerOfMovieResult = GenreManager.GetSearchContainerByGenreId(genre.Id, Page);
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

        public const string MovieListVisibilityPropertyName = "MovieListVisibility";
        private Visibility _movieListVisibility;
        public Visibility MovieListVisibility {
            get { return _movieListVisibility; }
            set { _movieListVisibility = value; RaisePropertyChanged(MovieListVisibilityPropertyName); }
        }

        public const string SelectedMovieVisibilityPropertyName = "SelectedMovieVisibility";
        private Visibility _selectedMovieVisibility;
        public Visibility SelectedMovieVisibility {
            get { return _selectedMovieVisibility; }
            set { _selectedMovieVisibility = value; RaisePropertyChanged(SelectedMovieVisibilityPropertyName); }
        }

        public const string GenreListVisibilityPropertyName = "GenreListVisibility";
        private Visibility _genreListVisibility;
        public Visibility GenreListVisibility {
            get { return _genreListVisibility; }
            set { _genreListVisibility = value; RaisePropertyChanged(GenreListVisibilityPropertyName); }
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
        private Movie _selectedMovie;
        public Movie SelectedMovie {
            get { return _selectedMovie; }
            set { _selectedMovie = value; RaisePropertyChanged(SelectedMoviePropertyName); }
        }

        public const string SelectedGenrePropertyName = "SelectedGenre";
        private Genre _selectedGenre;
        public Genre SelectedGenre {
            get { return _selectedGenre; }
            set { _selectedGenre = value; RaisePropertyChanged(SelectedGenrePropertyName); }
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

        public const string GenresPropertyName = "Genres";
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres {
            get { return _genres; }
            set { _genres = value; RaisePropertyChanged(GenresPropertyName); }
        }

        public const string CustomSearchPropertyName = "CustomSearch";
        private string _customSearch;
        public string CustomSearch {
            get { return _customSearch; }
            set { _customSearch = value; RaisePropertyChanged(CustomSearchPropertyName); }
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
            ToggleSearchVisibilityCommand = new RelayCommand(ToggleSearchVisibilityAction);
            ToggleMovieListVisibilityCommand = new RelayCommand(ToggleMovieListVisibilityAction);
            ToggleSelectedMovieVisibilityCommand = new RelayCommand(ToggleSelectedMovieVisibilityAction);
            ToggleGenreListVisibilityCommand = new RelayCommand(ToggleGenreListVisibilityAction);
            SetMinimizeWindowStateCommand = new RelayCommand<object>(SetMinimizeWindowStateAction);
            DragMoveCommand = new RelayCommand<object>(DragMoveAction);
            SetSearchStateCommand = new RelayCommand<string>(SetSearchStateAction);
            SetSelectedMovieCommand = new RelayCommand<object>(SetSelecteMovieAction);
            SetSelectedGenreCommand = new RelayCommand<object>(SetSelectedGenreAction);
            IncrementPageCommand = new RelayCommand(IncrementPageAction, CanIncrementPage);
            DecrementPageCommand = new RelayCommand(DecrementPageAction, CanDecrementPage);
            AddToMyMoviesCommand = new RelayCommand<int>(AddToMyMoviesAction, CanAddToMyMovies);
        }

        private void Init() {
            MovieManager = new MovieManager();
            GenreManager = new GenreManager();
            IsBusy = false;
            SearchVisibility = Visibility.Hidden;
            MovieListVisibility = Visibility.Hidden;
            GenreListVisibility = Visibility.Hidden;
            SelectedMovieVisibility = Visibility.Hidden;
            SearchState = SearchState.NowPlaying;
            SetSearchStateAction(SearchState.ToString());
            Genres = new ObservableCollection<Genre>(GenreManager.GetAll());
            CustomSearch = "";
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}