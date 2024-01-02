namespace GitNet.Components.LogViewer;

public class GitCommitViewModel(GitCommitModel model) : ViewModelBase
{
    public string Message { get; } = model.Message;
    public string Hash  { get; } = model.Hash;
    
    public string Author { get; } = model.Author;
    
    public string Committer { get; } = model.Committer;
    
    public string CommitterOrAuthor => Committer == Author ? Committer : $"{Committer} (original author: {Author})";
    
    public DateTimeOffset CommitDate { get; } = model.CommitDate;
    public string CommitDateOrTime
    {
        get
        {
            var now = DateTimeOffset.Now;
            var timeSpan = now - CommitDate;
            if (CommitDate.Year != now.Year)
            {
                return CommitDate.ToString("yyyy-MM-dd");
            }
            if (CommitDate.Month != now.Month || CommitDate.Day != now.Day)
            {
                return CommitDate.ToString("MM-dd");
            }
            return CommitDate.ToString("HH:mm");
        }
    }
}