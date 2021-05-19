// https://supportcenter.devexpress.com/ticket/details/t239143/auto-select-first-row-on-load
// a datagrid service which enable focusing the first row , even when rows get ordered
/*
	<dxg:GridControl  SelectionMode="Row" EnableSmartColumnsGeneration="True" ItemsSource="{Binding CompositionHistories}" ItemsSourceChanged="OnItemsSourceChanged" SelectedItem="{Binding SelectedHistory, Mode=TwoWay}" SourceUpdated="onSourceUpdated" >
		<dxmvvm:Interaction.Behaviors>
			<viewModels:DataGridService/>
		</dxmvvm:Interaction.Behaviors>
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;

namespace WeRockClient.ViewModels
{
	public interface IDataGridService
	{
        void FocusFirstRow();
        void FocusRow(object value);
	}



	public class DataGridService:ServiceBase,IDataGridService
	{ 
		void IDataGridService.FocusFirstRow()
        {
            GridControl grid = ((GridControl)AssociatedObject);
            grid.View.FocusedRowHandle = 0;
            grid.SelectedItem = grid.GetRow(0);
        }
	}
}
