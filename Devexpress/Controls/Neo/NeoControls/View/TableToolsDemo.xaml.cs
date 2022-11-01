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
using DevExpress.Mvvm;
using NeoTrader;

namespace NeoControls
{
    public class Persion: BindableBase
    {
        public string Name { get => GetProperty(() => Name); set => SetProperty(() => Name, value); }
        public int Age { get => GetProperty(() => Age); set => SetProperty(() => Age, value); }
    }
    /// <summary>
    /// TableToolsDemo.xaml 的交互逻辑
    /// </summary>
    public partial class TableToolsDemo : UserControl
    {
        public int SelectedIdx { get; set; } 
        public ObservableCollection<Persion> Persions { get; set; } 
        public ObservableCollection<CommandVm> DefaultTools { get; set; }
        public TableToolsDemo()
        {
            InitializeComponent();
            this.DataContext = this;
            InitData();
        }

        public void InitData()
        {
            Persions = new ObservableCollection<Persion>();
            for(int i = 0;i < 10; i++)
            {
                Persions.Add(new Persion() { Name = $" 张三{i}", Age = i });
            }

            DefaultTools =new ObservableCollection<CommandVm>()
            {
                new CommandVm(){ DisplayName = "X", Command = new DelegateCommand(() => 
                {
                    Persions.RemoveAt(SelectedIdx);
                })},

                new CommandVm(){ DisplayName = "+", Command = new DelegateCommand(() =>
                {
                    Persions.Add(new Persion(){ Name = "李四", Age = 70 } );
                })},
                new CommandVm(){ Glyph=Images.VMore, Commands = new ObservableCollection<CommandVm>()
                {
                    new CommandVm(){ Glyph= Images.Watchlist, DisplayName="查看List", Command = new DelegateCommand(()=>{ }) },
                    new CommandVm(){ Glyph = Images.Account, DisplayName = "用户信息", Command = new DelegateCommand(()=>{ }) },
                    new CommandVm(){ Glyph = Images.Trading, DisplayName = "交易", Command = new DelegateCommand(() => { }) },
                }}
            };
            
        }
    }
}
