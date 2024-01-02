namespace GitNet;

public class MainWindowViewModel : ViewModelBase, IMainWindowContentChooser
{
    public double WindowWidth => CurrentContentViewModel.WindowWidth;
    public double WindowHeight => CurrentContentViewModel.WindowHeight;
    public double WindowMaxWidth => CurrentContentViewModel.WindowMaxWidth;
    public double WindowMaxHeight => CurrentContentViewModel.WindowMaxHeight;
    public double WindowMinWidth => CurrentContentViewModel.WindowMinWidth;
    public double WindowMinHeight => CurrentContentViewModel.WindowMinHeight;
    
    public string CurrentSolutionPath
    {
        get => Log.SolutionPath;
        set => Log.SolutionPath = value;
    }

    public string CurrentGitRepoPath 
    {
        get => Log.GitRepoPath;
        set => Log.GitRepoPath = value;
    }
    
    private Visibility _projectChooserVisibility = Visibility.Visible;
    private Visibility _logVisibility = Visibility.Collapsed;
    private MainWindowContent _currentContent = MainWindowContent.ProjectChooser;

    public MainWindowViewModel()
    {
        Log = new LogViewModel();
        ProjectChooser = new ProjectChooserDialogViewModel(
            new GitRepoPathValidator(),
            new SolutionPathValidator(),
            this);
        ProjectChooser.Initialize();
    }

    public ProjectChooserDialogViewModel ProjectChooser { get; }

    public IMainWindowContent CurrentContentViewModel
    {
        get
        {
            return CurrentContent switch
            {
                MainWindowContent.ProjectChooser => ProjectChooser,
                MainWindowContent.Log => Log,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
    public Visibility ProjectChooserVisibility
    {
        get => _projectChooserVisibility;
        set
        {
            _projectChooserVisibility = value;
            OnPropertyChanged();
        }
    }

    public LogViewModel Log { get; }
    
    public Visibility LogVisibility
    {
        get => _logVisibility;
        set
        {
            _logVisibility = value;
            OnPropertyChanged();
        }
    }
    
    public enum MainWindowContent
    {
        ProjectChooser,
        Log,
    }

    public MainWindowContent CurrentContent
    {
        get => _currentContent;
        set
        {
            if (_currentContent == value)
            {
                return;
            }
            
            _currentContent = value;
            switch (value)
            {
                case MainWindowContent.ProjectChooser:
                    ProjectChooserVisibility = Visibility.Visible;
                    LogVisibility = Visibility.Collapsed;
                    break;
                case MainWindowContent.Log:
                    ProjectChooserVisibility = Visibility.Collapsed;
                    LogVisibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            OnPropertyChanged(nameof(CurrentContentViewModel));
            OnPropertyChanged(nameof(WindowWidth));
            OnPropertyChanged(nameof(WindowHeight));
            OnPropertyChanged(nameof(WindowMaxWidth));
            OnPropertyChanged(nameof(WindowMaxHeight));
        }
    }
}

public interface IMainWindowContentChooser
{
    MainWindowViewModel.MainWindowContent CurrentContent { get; set; }
    string CurrentSolutionPath { get; set; }
    string CurrentGitRepoPath { get; set; }
}

public interface IMainWindowContent
{
    double WindowHeight { get; }
    double WindowWidth { get; }
    double WindowMaxHeight { get; }
    double WindowMaxWidth { get; }
    double WindowMinHeight { get; }
    double WindowMinWidth { get; }
}