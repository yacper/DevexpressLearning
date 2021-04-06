using DevExpress.Mvvm;
using System;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core.MvvmSample;
using MifCommon.Common;
using MifModules.ViewModels;
using RestoreState.ViewModels;
using Module = DevExpress.Mvvm.ModuleInjection.Module;

namespace MIFRestoreState.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public static MainViewModel Create()
		{
			return ViewModelSource.Create(() => new MainViewModel());
		}

		public void AddModule()
		{
			int x = 1;
			while (ModuleManager.DefaultManager.GetModule(Regions.Navigation, $"Module{x}") != null)
			{
				x++;
			}

			ModuleManager.DefaultManager.Register(Regions.Documents, new Module($"Module{x}", () => ModuleViewModel.Create($"Module{x}", $"Module{x} Content"), typeof(ModuleView)));
			ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Navigation, new Module($"Module{x}", () => new NavigationItem($"Module{x}")));
		}
	}
}