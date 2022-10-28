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

namespace NeoDataGrid.TestView
{
    public class Child
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Persion
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public ObservableCollection<Child> Childs { get; set; }
    }

    /// <summary>
    /// RGridControlTest.xaml 的交互逻辑
    /// </summary>
    public partial class RGridControlTest : UserControl
    {
        public ObservableCollection<Persion> Persions { get; set; }
        public RowTools RowTools { get; set; }

        public RGridControlTest()
        {
            InitializeComponent();
            DataContext = this;

            RowTools = CreateRowTools();
            Persions = new ObservableCollection<Persion>();

            InitData();
            //_RGridControl.RItemSource = Persions;
            Persions.CollectionChanged += Persions_CollectionChanged;
        }

        private void Persions_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // 集合发生变化
        }

        private void InitData()
        {
            for(int i = 0; i < 10; i++)
            {
                Persions.Add(new Persion() 
                {
                    Name = i.ToString(),
                    Age = i
                });
            }
        }

        private RowTools CreateRowTools()
        {
            RowTools rowTools = new RowTools(true);
            rowTools.AddVM(new RowToolsViewMode() 
            {
                //IsNormalUse = true,
                DisplayMode = DisplayMode.Glyph,
                 Glyph = Images.Build,
                 DisplayName = "编译",
                 Commands = new ObservableCollection<CommandViewModel>()
                 {
                     new RowToolsViewMode(){ DisplayName = "张三", Glyph = Images.Error },
                     new RowToolsViewMode(){ DisplayName = "里斯", Glyph = Images.Error }
                 }
            });

            rowTools.AddVM(new RowToolsViewMode()
            {
                IsNormalUse = true,
                DisplayMode = DisplayMode.Content,
                Glyph = Images.Build,
                DisplayName = "+",
               // DisplayTextWidth = 30
            });
            rowTools.AddVM(new RowToolsViewMode()
            {
                //IsNormalUse = true,
                DisplayMode = DisplayMode.Glyph,
                Glyph = Images.Cut,
                DisplayName = "剪切",
                IsEnabled = false
                
            });
            rowTools.AddVM(new RowToolsViewMode()
            {
                IsNormalUse = true,
                DisplayMode = DisplayMode.Glyph,
                Glyph = Images.Copy,
                DisplayName = "复制"
            });

            return rowTools;
        }
        
    }
}
