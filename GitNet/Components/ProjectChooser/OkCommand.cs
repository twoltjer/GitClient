namespace GitNet.Components.ProjectChooser;

public class OkCommand : ICommand
{
    private readonly ProjectChooserDialogViewModel _viewModel;
    private readonly IMainWindowContentChooser _mainWindowContentChooser;

    public OkCommand(ProjectChooserDialogViewModel viewModel, IMainWindowContentChooser mainWindowContentChooser)
    {
        _viewModel = viewModel;
        _mainWindowContentChooser = mainWindowContentChooser;
        _viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(_viewModel.HasErrors))
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        };
    }

    public bool CanExecute(object? parameter)
    {
        return !_viewModel.HasErrors;
    }

    public void Execute(object? parameter)
    {
        _mainWindowContentChooser.CurrentContent = MainWindowViewModel.MainWindowContent.Log;
    }

    public event EventHandler? CanExecuteChanged;
}