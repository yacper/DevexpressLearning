using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeoTrader.UI.Controls
{
    public class RColumnItemData
    {
        public string FieldName { get; set; }
        public object Header { get; set; }
        public bool IsFixed { get; set; }
        public bool AllowSorting { get; set; }
        //public DataTemplate CellTemplate { get; set; }
        public GridColumnWidth Width { get; set; }

        public RColumnItemData(string fieldName)
        {
            AllowSorting = true;
            FieldName = fieldName;            
            Width = new GridColumnWidth(0, GridColumnUnitType.Auto);
        }
    }
}
