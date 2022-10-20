using NeoTrader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NeoDataGrid.Control
{
    public class CombineDataItem : IRowTools            // 组合数据
    {
        #region RowTool
        public ObservableCollection<RowToolsViewMode> ToolVMs => RowTools.ToolVMs;

        public RowToolsViewMode SelectedToolVm
        {
            get => RowTools.SelectedToolVm;
            set
            {
                RowTools.SelectedToolVm = value;
            }
        }

        public bool ToolIsFixed => RowTools.ToolIsFixed;

        public Brush ToolsBgBrush => RowTools.ToolsBgBrush;
        #endregion

        public Object Source { get; set; }              // 真是的数据源
        public RowTools RowTools { get; set; }

        public CombineDataItem(object source, RowTools tools)
        {
            Source = source;
            RowTools = tools;
        }
    }
}
