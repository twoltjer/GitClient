namespace GitNet;

public class NotifyDataErrorInfoBase : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == null)
        {
            return _errors.SelectMany(err => err.Value.ToList());
        }
        
        if (_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
        {
            return _errors[propertyName];
        }
        return null!;
    }

    public bool HasErrors => _errors.Any();

    protected void SetErrors(string propertyName, List<string> errors)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (errors == null || !errors.Any())
        {
            _errors.Remove(propertyName);
        }
        else
        {
            _errors[propertyName] = errors;
        }
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}