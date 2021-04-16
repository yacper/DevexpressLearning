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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core.ConditionalFormatting;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace ConditionalFormatingCodeBehind
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			view.FormatConditions.AddRange(new List<FormatConditionBase> {
				new FormatCondition() {
					Expression = "[SalesVsTarget] < 0.0m",
					FieldName = "SalesVsTarget",
					PredefinedFormatName = "RedText"
				},
				new FormatCondition() {
					Expression = "[Profit] < 0.0",
					FieldName = "Profit",
					Format = new Format() {
						Foreground = Brushes.Red
					}
				},
				new DataBarFormatCondition() {
					FieldName = "Sales",
					PredefinedFormatName = "RedGradientDataBar"
				},
				new TopBottomRuleFormatCondition() {
					Expression = "[Sales]",
					FieldName = null,
					PredefinedFormatName = "BoldText",
					Rule = TopBottomRule.TopPercent,
					Threshold = 10d
				},
				new DataBarFormatCondition() {
					FieldName = "Profit",
					PredefinedFormatName = "GreenGradientDataBar"
				},
				new IconSetFormatCondition() {
					FieldName = "MarketShare",
					PredefinedFormatName = "Quarters5IconSet"
				}
			});
		}
	}
}
