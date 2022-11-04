using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NeoControls;

namespace  NeoTrader.UI.Controls
{
    public class RToolBar : ToolBarControl
    {
        public RToolBar()
        {
            // 提供默认值
            UseWholeRow             = true;
            AllowCustomizationMenu  = false;
            AllowQuickCustomization = false;
            RotateWhenVertical      = true;
            ShowDragWidget          = false;
            ItemTemplateSelector    = (DataTemplateSelector)App.Current.FindResource("BarItemDataTemplateSelector");
        }
    }
}
