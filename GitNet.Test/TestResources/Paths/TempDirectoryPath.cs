namespace GitNet.Test.TestResources.Paths;

public class TempDirectoryPath : PathBase, IDisposable
{
    private readonly string _path;

    public TempDirectoryPath()
    {
        _path = GetTempDirPath();
        Directory.CreateDirectory(_path);
    }

    public override string ToString()
    {
        return _path;
    }

    public void Dispose()
    {
        Directory.Delete(_path, true);
    }

    public static string GetTempDirPath()
    {
        return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    }
}