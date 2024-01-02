namespace GitNet.Components.LogViewer;

public class GitCommitModel(string message, string hash, string author, string committer, DateTimeOffset commitDate)
{
    public GitCommitModel(Commit commit) : this(
        commit.MessageShort,
        commit.Id.ToString(8),
        commit.Author.Name,
        commit.Committer.Name,
        commit.Committer.When)
    {
        
    }
    
    public string Message { get; } = message;

    public string Hash { get; } = hash;
    
    public string Author { get; } = author;
    
    public string Committer { get; } = committer;
    
    public DateTimeOffset CommitDate { get; } = commitDate;
}