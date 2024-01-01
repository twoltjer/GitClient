namespace GitNet;

public class DelegateCommand(Action executeMethod) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        executeMethod();
    }

    public event EventHandler? CanExecuteChanged;
}