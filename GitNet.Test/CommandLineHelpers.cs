namespace GitNet.Test;

public class CommandLineHelpers
{
    public static void ExecuteDotNetCommand(string workingDirectory, string arguments)
    {
        using var process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = arguments;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WorkingDirectory = workingDirectory;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception($"dotnet {arguments} failed with exit code {process.ExitCode}.\nOutput:\n{output}\nError:\n{error}");
        }
    }
}