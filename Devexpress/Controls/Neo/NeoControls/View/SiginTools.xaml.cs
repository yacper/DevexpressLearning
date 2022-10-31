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

    public class Privoider: BindableBase
    {
        public ConnectedStatus Status { get; set; } = ConnectedStatus.Disconnected;
        public virtual ImageSource Source { get => GetProperty(() => Source); set => SetProperty(() => Source, value); }

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
        public Privoider Privoider { get; set; }
        public ObservableCollection<CommandVm> PVms { get; set; }
        public SiginTools()
        {
            InitializeComponent();
            this.DataContext = this;
            InitData();
        }

        private void InitData()
        {
            Privoider = new Privoider();
            PVms = new ObservableCollection<CommandVm>() 
            {
                new CommandVm()
                {
                    DisplayName = "TCP_Privorder",
                    DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                    Glyph = Images.Monitor,
                    StateImg = Images.ConnectedStatus,
                    Command = new DelegateCommand(() =>
                    {
                        Privoider.Toggle();
                    })
                }.WithPropertyBinding<Privoider>(T=>T.StateImg, Privoider, S=>S.Source),
            };
        }
    }
}
