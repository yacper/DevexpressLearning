using DevExpress.Mvvm;
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

    public         string          Name   { get;                              set; } = "P1";
    public         ConnectedStatus Status { get;                              set; } = ConnectedStatus.Disconnected;
    public virtual ImageSource     Source { get => GetProperty(() => Source); set => SetProperty(() => Source, value); }

    public void Toggle()
    {
        Status = Status == ConnectedStatus.Disconnected ? ConnectedStatus.Connected : ConnectedStatus.Disconnected;
        Source = Status == ConnectedStatus.Disconnected ? null : Images.ConnectedStatus;
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
                //DisplayNameExpression = (vm) =>((vm.Owner as Provider).Name),
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
        .WithPropertyBinding(T => T.StateImg, S => (S.Owner as Provider).Source)
             .WithPropertyBinding(T => T.DisplayName, S => (S.Owner as Provider).Name);

            CommandVm vm2 = vm.Clone(new Provider() { Name = "p2" })
                    ;
            //vm2.DisplayName = "p2";


            //var vm2 = new CommandVm()
            //      {
            //          Owner = new Provider() { Name = "P2" },
            //          //DisplayName = ,
            //          //DisplayNameExpression = (vm) =>((vm.Owner as Provider).Name),
            //          DisplayMode = BarItemDisplayMode.ContentAndGlyph,
            //          Glyph       = Images.Monitor,
            //          //StateImg    = Images.ConnectedStatus,
            //          Command = new DelegateCommand<FrameworkContentElement>((e) =>
            //                                                                 {
            //                                                                     Console.WriteLine(e.ToString());
            //                                                                     ((e.DataContext as CommandVm).Owner as Provider).Toggle();
            //                                                                 }
            //                                                                )
            //      }
            //  //.WithPropertyBinding(T => T.StateImg, S => (S.Owner as Provider).Source)
            //       .WithPropertyBinding(T => T.DisplayName, S => (S.Owner as Provider).Name);


            ;

            //vm2.Command.Execute(null);


            PVms = new ObservableCollection<CommandVm>()
        {
            vm,
            vm2
        };


        //PVms = new ObservableCollection<CommandVm>()
        //{
        //    new CommandVm()
        //    {
        //        Owner       = new Provider(){Name = "P1"},
        //        //DisplayName = ,
        //        //DisplayNameExpression = (vm) =>((vm.Owner as Provider).Name),
        //        DisplayMode           = BarItemDisplayMode.ContentAndGlyph,
        //        Glyph                 = Images.Monitor,
        //        //StateImg    = Images.ConnectedStatus,
        //        Command = new DelegateCommand<FrameworkContentElement>((e) =>
        //                                                 {

        //                                                     Console.WriteLine(e.ToString());
        //                                                     ((e.DataContext as CommandVm).Owner as Provider).Toggle();
        //                                                 }
        //                                                )
        //    }.WithPropertyBinding(T => T.StateImg, S => (S.Owner as Provider).Source)
        //    .WithPropertyBinding(T => T.DisplayName, S => (S.Owner as Provider).Name)

        //    //.WithCommand(new DelegateCommand(() =>
        //    //                                             {

        //    //                                                 //Console.WriteLine(e.ToString());
        //    //                                                 base..Toggle();
        //    //                                             }
        //    //                                            )),


        //};
    }
}
}