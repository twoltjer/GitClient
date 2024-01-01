namespace GitNet.Test;

public class ActualFilePath : PathBase
{
    private readonly string _path;

    public ActualFilePath(string path)
    {
        _path = path;
    }

    public override string ToString()
    {
        return _path;
    }
}