using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeoDataGrid.Control
{
    public class RGridColumn: GridColumn
    {
        public static DependencyProperty RFieldNameProperty = DependencyProperty.Register("RFieldName", typeof(string), typeof(RGridColumn), 
            new PropertyMetadata(null, (d, e) => 
            {
                var rc = d as RGridColumn;
                rc!.SetFieldName((string)e.NewValue);
            }));

        public string RFieldName 
        { 
            get=>(string)GetValue(RFieldNameProperty);
            set
            {
                SetValue(RFieldNameProperty, value);
                SetFieldName(value);
            }
        }

        private void SetFieldName(string rFName)
        {
            FieldName = $"{nameof(CombineDataItem.Source)}.{rFName}";

            if (Header == null)
                Header = rFName;
        }


    }
}
