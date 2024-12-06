using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Messaging;

namespace MvvmToolkitTest
{
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.DataContext = new MainWindowViewModel() { Name = "OK" };
        InitializeComponent();
    }
}

public class MyMessage
{
    public string A { get; set; }
}

public partial class MainWindowViewModel : ObservableRecipient, IRecipient<MyMessage>
{
    public MainWindowViewModel() { Name = "a"; }

    public void Receive(MyMessage message)
    {
        // 处理消息

    }

    partial void OnNameChanged(string oldValue, string newValue) { Debug.WriteLine("Name changed from " + oldValue + " to " + newValue); }

    [ObservableProperty] protected string _Name;
}
}