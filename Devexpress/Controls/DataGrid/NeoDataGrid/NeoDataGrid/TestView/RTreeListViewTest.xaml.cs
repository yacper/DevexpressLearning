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
    /// <summary>
    /// RTreeListViewTest.xaml 的交互逻辑
    /// </summary>
    public partial class RTreeListViewTest : UserControl
    {
        public ObservableCollection<Persion> Persions { get; set; }
        public ObservableCollection<RowTools> RowTools { get; set; }
        public RTreeListViewTest()
        {
            InitializeComponent();
            this.DataContext = this;

            InitData();
        }


        private void InitData()
        {
            Persions = new ObservableCollection<Persion>();
            for (int i = 0; i < 10; i++)
            {
                Persions.Add(new Persion()
                {
                    Name = $"parent: {i}",
                    Age = i,
                    Childs = new ObservableCollection<Child>() { new Child() { Name=$"A child{i}", Age = i -1 }, new Child() { Name = $"B child{i}", Age = i - 1 } }
                });
            }

            RowTools = new ObservableCollection<RowTools>();
            RowTools.Add(new RowTools() 
            {
                Level = 0,                
            });

            RowTools.Add(new RowTools()
            {
                Level = 1,                
            });
        }
    }
}
