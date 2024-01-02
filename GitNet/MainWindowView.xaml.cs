namespace GitNet;

public partial class MainWindowView
{
    public MainWindowView()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}