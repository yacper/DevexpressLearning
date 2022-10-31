using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeoTrader.UI.Controls
{
    public class RBarSubItem: BarSubItem
    {
        public RBarSubItem()
        {
            Loaded += RBarSubItem_Loaded;
        }

        private void RBarSubItem_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Popup += RBarSubItem_Popup;
        }

        private void RBarSubItem_Popup(object sender, EventArgs e)
        {
            if (PopupIsInit)
                return;
            SubMenuClientPanel panel = NeoTrader.UiUtils.UIUtils.GetParentObject<SubMenuClientPanel>(ItemLinks[0].LinkControl as LightweightBarItemLinkControl);
            var borders = NeoTrader.UiUtils.UIUtils.GetChildObjects<Border>(panel, "PATA_BAR_Border");
            double maxVal = 0;
            foreach (var b in borders)
            {
                if (b.ActualWidth > maxVal)
                    maxVal = b.ActualWidth;
            }

            foreach (var b in borders)
            {
                b.Width = maxVal;
            }

            PopupIsInit = true;
        }

        private bool PopupIsInit = false;
    }
}
