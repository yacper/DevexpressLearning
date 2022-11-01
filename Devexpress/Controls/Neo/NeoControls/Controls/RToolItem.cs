using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NeoTrader.UI.Controls
{
public class RToolItem : ContentControl
{
    //public static DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(CommandVm), typeof(RToolItem),
    //                                                                               new PropertyMetadata(null, (d, e) => { (d as RToolItem).Reset(); }));

    //public CommandVm Command { get => (CommandVm)GetValue(CommandProperty); set { SetValue(CommandProperty, value); } }


    public RToolItem() : base() { this.Loaded += (s, e) => { Reset(); }; }

    private void Reset()
    {
        return;
        //if (!IsLoaded)
        //    return;

        //if (Command == null)
        //{
        //    Visibility = Visibility.Collapsed;
        //    return;
        //}

        //this.DataContext = Command;

        //if (Command.IsSeparator)
        //{
        //    Template = (ControlTemplate)Application.Current.FindResource("SeparatorTemplate");
        //    return;
        //}

        //if (Command.IsGlyphText)
        //    Template = (ControlTemplate)App.Current.FindResource("IconTextTemplate");

        //if (vm.IsToggleButton)
        //    Template = (ControlTemplate)App.Current.FindResource("ToggleBtnTemplate");

        //if (!vm.IsToggleButton && !vm.IsLinButton && !vm.IsGlyphText)
        //    Template = (ControlTemplate)App.Current.FindResource("NormalButtonTemplate");

        //if (vm.Commands == null || vm.Commands.Count == 0)
        //    return;

        //vm.Command = new DelegateCommand<UIElement>(e =>
        //{
        //    _ContextMenu                 = CreateRowControlContextMenu(vm.Commands); // 暂时: 所有的下拉菜单都是 Icon + DisplayName 样式
        //    _ContextMenu.PlacementTarget = e;
        //    _ContextMenu.Placement       = PlacementMode.Bottom;
        //    _ContextMenu.IsOpen          = true;
        //});
    }

    //private ContextMenu CreateRowControlContextMenu(IEnumerable<CommandViewModel> vms)
    //{
    //    ContextMenu contextMenu = new ContextMenu();
    //    foreach (var vm in vms)
    //        contextMenu.Items.Add(CreateMenuItem(vm));

    //    return contextMenu;
    //}

    //private MenuItem CreateMenuItem(CommandViewModel vm)
    //{
    //    MenuItem menuItem = new MenuItem()
    //    {
    //        Command          = vm.Command,
    //        Header           = vm.DisplayName,
    //        IsEnabled        = vm.IsEnabled,
    //        InputGestureText = $"{vm.KeyGesture}",
    //        Icon             = new Image() { StateImg = vm.Glyph },
    //        Height           = (double)App.Current.FindResource("ControlBoxContentSize"),
    //    };

    //    if (vm.Commands != null)
    //        foreach (var cmd in vm.Commands)
    //            menuItem.Items.Add(CreateMenuItem(cmd));

    //    return menuItem;
    //}

    private ContextMenu _ContextMenu;
}
}