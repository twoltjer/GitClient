namespace GitNet;

public class ProjectChooserDialogViewModel : NotifyDataErrorInfoBase
{
    private readonly GitRepoPathValidator _gitRepoPathValidator;
    private readonly SolutionPathValidator _solutionPathValidator;
    private string _gitRepoPath = string.Empty;
    private string _gitRepoErrors = string.Empty;
    private Visibility _gitRepoErrorsVisibility;
    private string _solutionPathErrors = string.Empty;
    private string _solutionPath = string.Empty;
    private Visibility _solutionPathErrorsVisibility;
    
    public ProjectChooserDialogViewModel(GitRepoPathValidator gitRepoPathValidator, SolutionPathValidator solutionPathValidator)
    {
        _gitRepoPathValidator = gitRepoPathValidator;
        _solutionPathValidator = solutionPathValidator;
        BrowseGitRepoCommand = new DelegateCommand(BrowseGitRepo);
        BrowseSolutionCommand = new DelegateCommand(BrowseSolution);
    }
    
    public string WindowTitle => AppConstants.AppName;
    public string GitRepoPromptText => "Choose a git repository to open:";
    public string SolutionPromptText => "Choose a solution file to open:";
    public int WindowWidth => 300;
    public int WindowHeight => 200;

    public string GitRepoPath
    {
        get => _gitRepoPath;
        set
        {
            _gitRepoPath = value;
            ValidateGitRepoPath(value);
            OnPropertyChanged();
        }
    }

    public string SolutionPath
    {
        get => _solutionPath;
        set
        {
            _solutionPath = value;
            ValidateSolutionPath(value);
            OnPropertyChanged();
        }
    }

    public ICommand BrowseGitRepoCommand { get; }
    public ICommand BrowseSolutionCommand { get; }

    private void BrowseGitRepo()
    {
        var dialog = new OpenFolderDialog();
        var result = dialog.ShowDialog();
        if (result.HasValue && result.Value)
        {
            GitRepoPath = dialog.FolderName;
        }
    }

    private void BrowseSolution()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Solution files (*.sln)|*.sln",
            Multiselect = false
        };
        var result = dialog.ShowDialog();
        if (result.HasValue && result.Value)
        {
            SolutionPath = dialog.FileName;
        }
    }
    
    private void ValidateGitRepoPath(string value)
    {
        var errors = _gitRepoPathValidator.GetErrors(value);
        SetErrors(nameof(GitRepoPath), errors.Any() ? errors.ToList() : null!);
        GitRepoErrors = errors.Any() ? string.Join(Environment.NewLine, errors) : string.Empty;
        GitRepoErrorsVisibility = errors.Any() ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ValidateSolutionPath(string value)
    {
        var errors = _solutionPathValidator.GetErrors(value);
        SetErrors(nameof(SolutionPath), errors.Any() ? errors.ToList() : null!);
        SolutionPathErrors = errors.Any() ? string.Join(Environment.NewLine, errors) : string.Empty;
        SolutionPathErrorsVisibility = errors.Any() ? Visibility.Visible : Visibility.Collapsed;
    }

    public Visibility GitRepoErrorsVisibility
    {
        get => _gitRepoErrorsVisibility;
        set
        {
            _gitRepoErrorsVisibility = value;
            OnPropertyChanged();
        }
    }

    public Visibility SolutionPathErrorsVisibility
    {
        get => _solutionPathErrorsVisibility;
        set
        {
            _solutionPathErrorsVisibility = value;
            OnPropertyChanged();
        }
    }

    public string GitRepoErrors
    {
        get => _gitRepoErrors;
        set
        {
            _gitRepoErrors = value;
            OnPropertyChanged();
        }
    }

    public string SolutionPathErrors
    {
        get => _solutionPathErrors;
        set
        {
            _solutionPathErrors = value;
            OnPropertyChanged();
        }
    }

    public void Initialize()
    {
        ValidateGitRepoPath(GitRepoPath);
        ValidateSolutionPath(SolutionPath);
    }
}