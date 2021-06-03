using DevExpress.Mvvm.POCO;

namespace MIF.Main.ViewModels
{
	public class MainViewModel
	{
		public static MainViewModel Create()
		{
			return ViewModelSource.Create(() => new MainViewModel());
		}
	}
}
