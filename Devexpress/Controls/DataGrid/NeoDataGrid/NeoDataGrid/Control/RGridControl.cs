using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using NeoTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NeoDataGrid.Control
{
    public class RGridControl : GridControl
    {
        // Tools 数据
        public static DependencyProperty RowToolsProperty = DependencyProperty.Register("RowTools", typeof(IEnumerable<IRowTools>), typeof(RGridControl), 
            new PropertyMetadata(null, (d,e) => 
            {
                //var rc = d as RGridControl;
                //rc!.RowTools = (IEnumerable<IRowTools>)e.NewValue;
            }));

        public IEnumerable<IRowTools> RowTools 
        {
            get => (IEnumerable<IRowTools>)GetValue(RowToolsProperty);
            set => SetValue(RowToolsProperty, value);            
        }

        public static DependencyProperty DefaultToolsProperty = DependencyProperty.Register("DefaultTools", typeof(IRowTools), typeof(RGridControl));

        public IRowTools DefaultTools 
        { 
            get => (IRowTools)GetValue(DefaultToolsProperty); 
            set => SetValue(DefaultToolsProperty, value);
        }                      

        public RGridControl(): base()
        {
            DefaultTools = new RowTools();
        }
    }
}
