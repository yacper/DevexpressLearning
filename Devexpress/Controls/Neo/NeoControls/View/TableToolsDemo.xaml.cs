using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public Persion()
        {
           // this.PropertyChanged += Persion_PropertyChanged;
        }

        public void AddAge()
        {
            Age++;
        }

        public void ReduceAge()
        {
            Age--;
        }

        private void Persion_PropertyChanged(PropertyChangedEventArgs e, ref CommandVm vm)
        {
            // 
            //if(e.PropertyName == nameof(Name))
            //{
            //    vm.DisplayMode
            //}
        }
    }
    /// <summary>
    /// TableToolsDemo.xaml 的交互逻辑
    /// </summary>
    public partial class TableToolsDemo : UserControl
    {
        public int SelectedIdx { get; set; } 
        public ObservableCollection<Persion> Persions { get; set; } 
        public CommandVm DefaultTools { get; set; }
        public Action<object, string, CommandVm> PropertyChangedAction { get; set; }
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

            DefaultTools = new CommandVm()
            {
                Commands = new ObservableCollection<CommandVm>()
                {
                    new CommandVm()
                    { 
                        DisplayName = "-",
                        Owner = Persions[0],
                        Command = new DelegateCommand<FrameworkContentElement>((e) =>
                        {
                            ((e.DataContext as CommandVm).Owner as Persion).ReduceAge();
                        })
                    }.WithPropertyBinding(T=>T.IsEnabled, S=>(S.Owner as Persion).Age ),

                    new CommandVm()
                    { 
                        DisplayName = "+",
                        Owner = Persions[0],
                        Command = new DelegateCommand<FrameworkContentElement>((e) =>
                        {
                            ((e.DataContext as CommandVm).Owner as Persion).AddAge();
                        })
                    }.WithPropertyBinding(T=>T.IsEnabled, S=>(S.Owner as Persion).Age),
                    new CommandVm()
                    { 
                        Glyph=Images.VMore, 
                        Command= new DelegateCommand(() => { }),
                        Commands = new ObservableCollection<CommandVm>()
                        {
                            new CommandVm(){ Glyph= Images.Watchlist, DisplayName="查看List", Command = new DelegateCommand(()=>{ }) },
                            new CommandVm(){ Glyph = Images.Account, DisplayName = "用户信息", Command = new DelegateCommand(()=>{ }) },
                            new CommandVm(){ Glyph = Images.Trading, DisplayName = "交易", Command = new DelegateCommand(() => { }) },
                        }
                    }
                }
            };

            PropertyChangedAction = PropertyChangedFun;

        }

        public void PropertyChangedFun(object s, string propertyName, CommandVm vm)
        {
            //if(propertyName == nameof(Persion.Age))  // 这个细微操作感觉可以直接用 VM的 WithPropertyBinding 来实现
            //{
            //    vm.Commands[0].IsEnabled = false;
            //    vm.Commands[1].IsEnabled = false;
            //}
        }
    }
}
