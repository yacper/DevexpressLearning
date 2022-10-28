using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XamlTest
{
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        PrintLogicalTree(0, this);
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        PrintVisualTree(0, this);
    }

    void PrintLogicalTree(int depth, object obj)
    {

        // Print the object with preceding spaces that represent its depth
        Debug.WriteLine(new string(' ', depth) + obj);

        // Sometimes leaf nodes aren't DependencyObjects (e.g. strings)
        if (!(obj is DependencyObject)) return;

        // Recursive call for each logical child
        foreach (object child in LogicalTreeHelper.GetChildren(obj as DependencyObject))
            PrintLogicalTree(depth + 1, child);
    }

    void PrintVisualTree(int depth, DependencyObject obj)
    {
        // Print the object with preceding spaces that represent its depth
        Debug.WriteLine(new string(' ', depth) + obj);

        // Recursive call for each visual child
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
    }

    // Change the foreground to blue when the mouse enters the button
    void Button_MouseEnter(object sender, MouseEventArgs e)
    {
        Button b                    = sender as Button;
        if (b != null) b.Foreground = Brushes.Blue;
    }

    // Restore the foreground to black when the mouse exits the button
    void Button_MouseLeave(object sender, MouseEventArgs e)
    {
        Button b                    = sender as Button;
        if (b != null) b.Foreground = Brushes.Black;
    }

}
}
//Window window = null;
//using (FileStream fs = new FileStream("MyWindow.xaml", FileMode.Open, FileAccess.Read))
//{
//    // Get the root element, which we know is a Window
//    window = (Window)XamlReader.Load(fs);
//}

//System.Windows.LogicalTreeHelper


//var template = resourceDictionary["Button"] as DataTemplate;
//var control = template.LoadContent() as Button;