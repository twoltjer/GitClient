namespace GitNet.Components.LogViewer;

public class LogViewModel : ViewModelBase, IMainWindowContent
{
    private string _gitRepoPath = string.Empty;
    private string _solutionPath = string.Empty;
    public double WindowMaxWidth => double.PositiveInfinity;
    public double WindowMaxHeight => double.PositiveInfinity;
    public double WindowWidth => SystemParameters.WorkArea.Width;
    public double WindowHeight => SystemParameters.WorkArea.Height;
    public double WindowMinWidth => 600;
    public double WindowMinHeight => 800;
    
    private Repository? _repository = null;

    public LogViewModel()
    {
    }

    public string SolutionPath
    {
        get => _solutionPath;
        set
        {
            if (value == _solutionPath)
            {
                return;
            }
            _solutionPath = value;
            OnPropertyChanged();
        }
    }

    public string GitRepoPath
    {
        get => _gitRepoPath;
        set
        {
            if (value == _gitRepoPath)
            {
                return;
            }
            _gitRepoPath = value;
            _repository?.Dispose();
            _repository = new Repository(value);
            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentBranchName));
            OnPropertyChanged(nameof(CommitList));
        }
    }

    public string CurrentBranchName => _repository?.Head.FriendlyName ?? string.Empty;

    public List<GitCommitViewModel> CommitList =>
        GetBranchCommits().Take(300)
            .Select(c => new GitCommitViewModel(new GitCommitModel(c)))
            .ToList();

    private IReadOnlyList<Commit> GetBranchCommits()
    {
        var current = _repository?.Head.Tip;
        var commits = new List<Commit>();
        while (current != null)
        {
            commits.Add(current);
            current = current.Parents.FirstOrDefault();
        }
        return commits;
    }

    public GitCommitViewModel? SelectedCommit { get; set; }
}