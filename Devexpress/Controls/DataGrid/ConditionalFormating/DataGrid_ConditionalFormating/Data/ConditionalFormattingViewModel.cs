using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.Mvvm.Native;

namespace ConditionalFormatting
{
	public class ConditionalFormattingViewModel
	{
		public ConditionalFormattingViewModel() { Items = SaleOverviewDataGenerator.GenerateSales().ToObservableCollection(); }
		public ObservableCollection<SaleOverviewData> Items { get;  set; }
	}
}