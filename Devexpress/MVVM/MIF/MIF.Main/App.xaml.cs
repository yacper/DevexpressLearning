using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using MIF.Common;
using MIF.Main.ViewModels;
using MIF.Main.Views;
using MIF.Modules.ViewModels;
using MIF.Modules.Views;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection.Native;
using DotEx.Common;
using AppModules = MIF.Common.Modules;

namespace MIF.Main
{
	public partial class App : Application
	{
		public App()
		{
			ApplicationThemeHelper.UpdateApplicationThemeName();
			SplashScreenManager.CreateThemed().ShowOnStartup();
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Bootstrapper.Run();
		}
		protected override void OnExit(ExitEventArgs e)
		{
			ApplicationThemeHelper.SaveApplicationThemeName();
			base.OnExit(e);
		}
	}
	public class Bootstrapper
	{
		public static Bootstrapper Default { get; protected set; }
		public static void Run()
		{
			Default = new Bootstrapper();
			Default.RunCore();
		}
		protected Bootstrapper() { }

		const string StateVersion = "1.0";
		public virtual void RunCore()
		{
			ConfigureTypeLocators();
			RegisterModules();
			if (!RestoreState())
				InjectModules();
			ConfigureNavigation();
			ShowMainWindow();
		}

		protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
		protected virtual void ConfigureTypeLocators()
		{
			var mainAssembly = typeof(MainViewModel).Assembly;
			var modulesAssembly = typeof(ModuleViewModel).Assembly;
			var assemblies = new[] { mainAssembly, modulesAssembly };
			ViewModelLocator.Default = new ViewModelLocator(assemblies);
			ViewLocator.Default = new ViewLocator(assemblies);
		}
		protected virtual void RegisterModules()
		{
			Manager.GetRegion(Regions.Documents).VisualSerializationMode = VisualSerializationMode.PerKey;
			Manager.Register(Regions.MainWindow, new Module(AppModules.Main, MainViewModel.Create, typeof(MainView)));
			Manager.Register(Regions.Navigation, new Module(AppModules.Module1, () => new NavigationItem("Module1")));
			Manager.Register(Regions.Navigation, new Module(AppModules.Module2, () => new NavigationItem("Module2")));
			Manager.Register(Regions.Documents, new Module(AppModules.Module1, () => ModuleViewModel.Create("Module1"), typeof(ModuleView)));
			Manager.Register(Regions.Documents, new Module(AppModules.Module2, () => ModuleViewModel.Create("Module2"), typeof(ModuleView)));
		}
		protected virtual bool RestoreState()
		{
			StateConfig con = ("StateConfig.json").FileToJsonObj<StateConfig>();
			if (con == null)
				return false;

			//var logicalInfo = LogicalInfo.Deserialize(con.LogicalState);
			//foreach (var region in logicalInfo.Regions)
			//{
			//	foreach (var regionItem in region.Items)
			//	{
			//		if (Manager.GetModule(region.RegionName, regionItem.Key) != null)
			//			continue;
			//		Manager.Register(region.RegionName, new Module(regionItem.Key, regionItem.ViewModelName, regionItem.ViewName));
			//	}
			//}
			return Manager.Restore(con.LogicalState, con.VisualState);



#if !DEBUG
			if (Settings.Default.StateVersion != StateVersion) return false;
			return Manager.Restore(Settings.Default.LogicalState, Settings.Default.VisualState);
#else
			return false;
#endif
		}
		protected virtual void InjectModules()
		{
			Manager.Inject(Regions.MainWindow, AppModules.Main);
			Manager.Inject(Regions.Navigation, AppModules.Module1);
			Manager.Inject(Regions.Navigation, AppModules.Module2);
		}
		protected virtual void ConfigureNavigation()
		{
			Manager.GetEvents(Regions.Navigation).Navigation += OnNavigation;
			Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
		}
		protected virtual void ShowMainWindow()
		{
			App.Current.MainWindow = new MainWindow();
			App.Current.MainWindow.Show();
			App.Current.MainWindow.Closing += OnClosing;
		}
		void OnNavigation(object sender, NavigationEventArgs e)
		{
			if (e.NewViewModelKey == null) return;
			Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
		}
		void OnDocumentsNavigation(object sender, NavigationEventArgs e)
		{
			Manager.Navigate(Regions.Navigation, e.NewViewModelKey);
		}
		void OnClosing(object sender, CancelEventArgs e)
		{
			string logicalState;
			string visualState;
			Manager.Save(out logicalState, out visualState);

			StateConfig con = new StateConfig();
			con.LogicalState = logicalState;
			con.VisualState = visualState;

			con.ToJsonFile("StateConfig.json");
		}
		public class StateConfig
		{
			public string LogicalState { get; set; }
			public string VisualState { get; set; }

		}
	}
}