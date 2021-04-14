using DevExpress.Mvvm;
using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using MIFRestoreState2.Views;

namespace MIFRestoreState2.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public static MainViewModel Create()
		{
			return ViewModelSource.Create(() => new MainViewModel());
		}

		[Command]
		public void AddModule()
		{
			int x = 1;
			while (ModuleManager.DefaultManager.GetModule(Regions.Navigation, $"Module{x}") != null)
			{
				x++;
			}

			ModuleManager.DefaultManager.Register(Regions.Documents, new Module($"Module{x}", () => ModuleViewModel.Create($"Module{x}", $"Module{x} Content"), typeof(ModuleView)));
			//ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Navigation, new Module($"Module{x}", () => new NavigationItem($"Module{x}")));
		}
	}
}