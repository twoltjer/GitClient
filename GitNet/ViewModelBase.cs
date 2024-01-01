namespace GitNet;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        VerifyPropertyName(propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [Conditional("DEBUG"), DebuggerStepThrough]
    private void VerifyPropertyName(string propertyName)
    {
        // Verify that the property name matches a real, 
        // public, instance property on this object. 
        if (TypeDescriptor.GetProperties(this)[propertyName] == null)
        {
            string msg = "Invalid property name: " + propertyName;
            Debug.Fail(msg);
        }
    }
}