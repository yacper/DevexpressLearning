﻿using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using NeoTrader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NeoControls
{
public enum ConnectedStatus
{
    Connected,
    Disconnected,
}

public class Provider : BindableBase
{
    public override string ToString() => Name;

    public         string          Name     { get;                                set; } = "P1";
    public         ConnectedStatus Status   { get;                                set; } = ConnectedStatus.Disconnected;
    public virtual ImageSource     StateImg { get => GetProperty(() => StateImg); set => SetProperty(() => StateImg, value); }
    public         int          Badge    { get => GetProperty(() => Badge);    set => SetProperty(() => Badge, value); }

    public void Toggle()
    {
        Status   =  Status == ConnectedStatus.Disconnected ? ConnectedStatus.Connected : ConnectedStatus.Disconnected;
        StateImg =  Status == ConnectedStatus.Disconnected ? null : Images.ConnectedStatus;
        Badge    += 1;
    }
}

/// <summary>
/// SiginTools.xaml 的交互逻辑
/// </summary>
public partial class SiginTools : UserControl
{
    public ObservableCollection<CommandVm> PVms { get; set; }

    public SiginTools()
    {
        InitializeComponent();
        this.DataContext = this;
        InitData();
    }

    private void InitData()
    {
        var vm = new CommandVm()
                 {
                     Owner = new Provider() { Name = "P1" },
                     //DisplayName = ,
                     DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                     Glyph       = Images.Monitor,
                     //StateImg    = Images.ConnectedStatus,
                     Command = new DelegateCommand<FrameworkContentElement>((e) =>
                                                                            {
                                                                                Console.WriteLine(e.ToString());
                                                                                ((e.DataContext as CommandVm).Owner as Provider).Toggle();
                                                                            }
                                                                           )
                 }
                 .WithPropertyBinding(T => T.StateImg, S => (S.Owner as Provider).StateImg)
                 .WithPropertyBinding(T => T.DisplayName, S => (S.Owner as Provider).Name)
                 .WithPropertyBinding(T => T.BadgeContent, S => (S.Owner as Provider).Badge);

        CommandVm vm2 = vm.Clone(new Provider() { Name = "p2" })
            ;


        PVms = new ObservableCollection<CommandVm>()
        {
            vm,
            vm2
        };
    }
}
}