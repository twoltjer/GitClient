namespace GitNet;

public class MainWindowViewModel : ViewModelBase, IMainWindowContentChooser
{
    public int WindowWidth => CurrentContentViewModel.WindowWidth;
    public int WindowHeight => CurrentContentViewModel.WindowHeight;
    
    private Visibility _projectChooserVisibility = Visibility.Visible;
    private Visibility _logVisibility = Visibility.Collapsed;
    private MainWindowContent _currentContent = MainWindowContent.ProjectChooser;

    public MainWindowViewModel()
    {
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

    public LogViewModel Log { get; } = new LogViewModel();
    
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
        Log
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
        }
    }
}

public interface IMainWindowContentChooser
{
    MainWindowViewModel.MainWindowContent CurrentContent { get; set; }
}

public interface IMainWindowContent
{
    int WindowWidth { get; }
    int WindowHeight { get; }
}