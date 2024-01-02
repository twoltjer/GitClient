using GitNet.Components.ProjectChooser;
using GitNet.GitInterfacing;
using GitNet.SolutionInterfacing;
using GitNet.Test.TestResources.Paths;
using NSubstitute;

namespace GitNet.Test;

public class ProjectChooserDialogViewModelTests
{
    public static IEnumerable<object?[]> MemberData()
    {
        yield return [null, "Path is required", null, "Path is required"];
        
        const string tempSolutionFileName = "Temp.sln";
        
        var tempDirectoryPath = new TempDirectoryPath();
        var tempSlnPath = Path.Combine(tempDirectoryPath, tempSolutionFileName);
        yield return [tempDirectoryPath, "Directory is not a git repository (DirectoryInfo)", new ActualFilePath(tempSlnPath), "File does not exist"];
        
        tempDirectoryPath = new TempDirectoryPath();
        var tempGitRepoPath = Path.Combine(tempDirectoryPath, ".git");
        Directory.CreateDirectory(tempGitRepoPath);
        yield return [tempDirectoryPath, "Directory is not a git repository (LibGit2Sharp)", new ActualFilePath(tempSlnPath), "File does not exist"];
        
        tempDirectoryPath = new TempDirectoryPath();
        tempSlnPath = Path.Combine(tempDirectoryPath, tempSolutionFileName);
        CommandLineHelpers.ExecuteDotNetCommand(tempDirectoryPath, $"new sln -n {Path.GetFileNameWithoutExtension(tempSolutionFileName)}");
        yield return [tempDirectoryPath, "Directory is not a git repository (DirectoryInfo)", new ActualFilePath(tempSlnPath), null];
        
        tempDirectoryPath = new TempDirectoryPath();
        Repository.Init(tempDirectoryPath);
        tempSlnPath = Path.Combine(tempDirectoryPath, tempSolutionFileName);
        CommandLineHelpers.ExecuteDotNetCommand(tempDirectoryPath, $"new sln -n {Path.GetFileNameWithoutExtension(tempSolutionFileName)}");
        yield return [tempDirectoryPath, null, new ActualFilePath(tempSlnPath), null];
    }
    
    
    [Theory]
    [MemberData(nameof(MemberData))]
    [Trait("Category", "System")]
    public void TestOpenGitRepositoryAndProject(PathBase? gitRepoPath, string? gitValidationError, PathBase? solutionPath, string? solutionValidationError)
    {
        var projectChooserDialogViewModel = new ProjectChooserDialogViewModel(
            new GitRepoPathValidator(), 
            new SolutionPathValidator(), Substitute.For<IMainWindowContentChooser>());
        projectChooserDialogViewModel.Initialize();
        
        if (gitRepoPath != null)
            projectChooserDialogViewModel.GitRepoPath = gitRepoPath;
        if (solutionPath != null)
            projectChooserDialogViewModel.SolutionPath = solutionPath;
        
        projectChooserDialogViewModel.HasErrors.Should().Be(gitValidationError != null || solutionValidationError != null);
        projectChooserDialogViewModel.GitRepoErrors.Should().Be(gitValidationError ?? string.Empty);
        projectChooserDialogViewModel.SolutionPathErrors.Should().Be(solutionValidationError ?? string.Empty);
    }
}