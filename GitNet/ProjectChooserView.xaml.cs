namespace GitNet;

public partial class ProjectChooserView
{
    public ProjectChooserView()
    {
        InitializeComponent();
        var projectChooserDialogViewModel = new ProjectChooserDialogViewModel(
            new GitRepoPathValidator(),
            new SolutionPathValidator());
        projectChooserDialogViewModel.Initialize();
        DataContext = projectChooserDialogViewModel;
    }
}