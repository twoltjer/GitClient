namespace GitNet.GitInterfacing;

public class GitRepoPathValidator
{
    public List<string> GetErrors(string path)
    {
        var errors = new List<string>();
        if (string.IsNullOrEmpty(path))
        {
            errors.Add("Path is required");
            return errors;
        }
        
        var directoryInfo = new DirectoryInfo(path);
        
        if (!directoryInfo.Exists)
        {
            errors.Add("Directory does not exist");
            return errors;
        }
        
        var subDirectories = directoryInfo.GetDirectories();
        if (subDirectories.All(x => x.Name != ".git"))
        {
            errors.Add("Directory is not a git repository (DirectoryInfo)");
            return errors;
        }

        try
        {
            using var repo = new Repository(path);
        }
        catch
        {
            errors.Add("Directory is not a git repository (LibGit2Sharp)");
        }
        
        return errors;
    }
}