namespace GitNet.Test;

public class PathBase
{
    public static implicit operator string(PathBase pathBase)
    {
        return pathBase.ToString();
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}