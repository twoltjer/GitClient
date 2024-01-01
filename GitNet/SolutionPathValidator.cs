namespace GitNet;

public class SolutionPathValidator
{
    public List<string> GetErrors(string path)
    {
        var errors = new List<string>();
        if (string.IsNullOrEmpty(path))
        {
            errors.Add("Path is required");
            return errors;
        }
        
        var fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
            errors.Add("File does not exist");
            return errors;
        }
        
        if (fileInfo.Extension != ".sln")
        {
            errors.Add("File is not a solution");
            return errors;
        }
        
        return errors;
    }
}