using System.ComponentModel;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using Autofac;
using DevExpress.Entity.Model;
using DevExpress.Mvvm.POCO;
using VisualStudioDockingCustomSerializationAutofac.ViewModels;
using DocumentViewModel = DevExpress.Xpf.CodeView.ViewModel.DocumentViewModel;

namespace VisualStudioDockingCustomSerializationAutofac {
    public partial class App : Application {
        static App() {
            DemoBaseControl.SetApplicationTheme();
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif

        public Autofac.IContainer Container { get; set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var builder = new Autofac.ContainerBuilder();


            builder.RegisterType<MainWindow>();        
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(MainViewModel))).As(typeof(MainViewModel)).SingleInstance();

            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(VisualStudioDockingCustomSerializationAutofac.ViewModels.DocumentViewModel))).As(typeof(VisualStudioDockingCustomSerializationAutofac.ViewModels.DocumentViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(ErrorListViewModel))).As(typeof(ErrorListViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(OutputViewModel))).As(typeof(OutputViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(PropertiesViewModel))).As(typeof(PropertiesViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(SearchResultsViewModel))).As(typeof(SearchResultsViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(SolutionExplorerViewModel))).As(typeof(SolutionExplorerViewModel));
            builder.RegisterType(ViewModelSource.GetPOCOType(typeof(ToolboxViewModel))).As(typeof(ToolboxViewModel));

            Container                   = builder.Build();
            ContainerProvider.Container = Container;

            var mainViewModel = Container.Resolve<MainViewModel>();
            var mainView = Container.Resolve<MainWindow>();
            mainView.DataContext   = mainViewModel;
            App.Current.MainWindow = mainView;

            mainView.Show();

        }
    }
}
