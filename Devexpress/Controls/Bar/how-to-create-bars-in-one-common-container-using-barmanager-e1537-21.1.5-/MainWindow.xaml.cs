using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.DataAnnotations;

namespace WpfApplication2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
    }


    public class MyViewModel {
        public virtual bool IsBold { get; set; }
        public virtual bool IsItalic { get; set; }
        public virtual bool IsUnderline { get; set; }
        public virtual FontWeight Weight { get; set; }
        public virtual TextDecorationCollection Decorations { get; set; }
        public virtual FontStyle Style { get; set; }                
        public virtual TextAlignment Alignment { get; set; }
        public virtual string Text { get; set; }
        
        public void OnIsBoldChanged() {
            Weight = IsBold ? FontWeights.Bold : FontWeights.Normal;
        }
        public void OnIsItalicChanged() {
            Style = IsItalic ? FontStyles.Italic : FontStyles.Normal;
        }
        public void OnIsUnderlineChanged() {
            Decorations = IsUnderline ? TextDecorations.Underline : new TextDecorationCollection();
        }        

        public MyViewModel() {
            Alignment = TextAlignment.Left;
            SetDefaultText();
        }

        void SetDefaultText() {
            Text = "Text";
        }

        // An OpenFileCommand will be generated from the following method by POCO.
        public void OpenFile() {
            SetDefaultText();
        }
        // A NewFileCommand will be generated from the following methods by POCO.
        public bool CanNewFile() {
            return true;
        }
        public void NewFile() {
            Text = null;
        }
        // A SetAlignmentCommand will be generated from the following method by POCO.
        public void SetAlignment(object parameter) {
            Alignment = ((TextAlignment)parameter);
        }

    }    
}
