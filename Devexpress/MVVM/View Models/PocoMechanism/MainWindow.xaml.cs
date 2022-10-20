using System.Windows;
using DevExpress.Xpf.Printing.Native;
using Example.ViewModel;

namespace Example {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var model = LoginViewModel.Create();
            var t     = model.GetType();
        }
    }
}
