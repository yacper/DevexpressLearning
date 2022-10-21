using NeoTrader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeoDataGrid.Control
{
    public static class RControlUtils
    {
        public static ContextMenu CreateRowControlContextMenu(ObservableCollection<RowToolsViewMode> vms)
        {
            ContextMenu contextMenu = new ContextMenu();
            foreach (var vm in vms)
                contextMenu.Items.Add(CreateMenuItem(vm));

            return contextMenu;

        }

        public static MenuItem CreateMenuItem(CommandViewModel vm)
        {
            MenuItem menuItem = new MenuItem()
            {
                Command = vm.Command,
                Header = vm.DisplayName,
                IsEnabled = vm.IsEnabled,
                InputGestureText = $"{vm.KeyGesture}",
                Icon = new Image() { Source = vm.Glyph },
                Height = (double)App.Current.FindResource("ToolSize"),
            };

            if (vm.Commands != null)
                foreach (var cmd in vm.Commands)
                    menuItem.Items.Add(CreateMenuItem(cmd));

            return menuItem;
        }
    }
}
