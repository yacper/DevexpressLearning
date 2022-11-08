using NeoTrader.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
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

namespace NeoControls.View
{
    /// <summary>
    /// DropDragTest.xaml 的交互逻辑
    /// </summary>
    public partial class DropDragTest : UserControl
    {
        public DropDragTest()
        {
            InitializeComponent();
            this.DataContext = new TableToolDemoVm();
        }

        private void RTreeListView_DragEnter(object sender, DragEventArgs e)
        {
            (this.DataContext as TableToolDemoVm).CollectionChangedInfo += $"DragEnter: {e.Source} {e.OriginalSource} \n";
            var treeListView = sender as RTreeListView;
            var rdg = treeListView.Parent as RDataGrid;
            e.Data.SetData("DataSource", rdg.SelectedItems);
        }

        private void RTreeListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  DragDrop.DoDragDrop(this, "", DragDropEffects.Copy);
        }

        private void RTreeListView_Drop(object sender, DragEventArgs e)
        {
            var treeListView = sender as RTreeListView;            
            MouseUpHandle = treeListView.GetRowHandleByTreeElement(e.OriginalSource as DependencyObject);

            if (MouseUpHandle < 0)
                return;
            
            var vm = this.DataContext as TableToolDemoVm;
            var datas = e.Data.GetData("DataSource") as IList;
            
            List<int> idxs = new List<int>();
            foreach (Person p in datas)
            {
                idxs.Add(vm.People.IndexOf(p));                
            }

            int i = 0;
            idxs.Sort();
            foreach (var idx in idxs)
            {
                vm.People.Move(idx - i, MouseUpHandle);
                i++;
            }
            
        }

        private void RTreeListView_DragOver(object sender, DragEventArgs e)
        {
            var tlv = sender as RTreeListView;
            var pt = e.GetPosition(tlv);
           // (this.DataContext as TableToolDemoVm).CollectionChangedInfo += $"{e.OriginalSource} \n";
            e.Effects = DragDropEffects.Move;
            
        }

        private void RTreeListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (this.DataContext as TableToolDemoVm).CollectionChangedInfo += $"MouseMove {e.OriginalSource} \n";
                DragDrop.DoDragDrop(this, "", DragDropEffects.Move);
            }
        }

        private void RTreeListView_DragLeave(object sender, DragEventArgs e)
        {
            //(this.DataContext as TableToolDemoVm).CollectionChangedInfo += $"DragLeave: {e.Source} {e.OriginalSource} \n";
            //e.Effects= DragDropEffects.None;
            //e.Handled = true;
        }

        int MouseUpHandle = -1;
        private void RTreeListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var treeListView = sender as RTreeListView;
            MouseUpHandle = treeListView.GetRowHandleByMouseEventArgs(e);
        }

        private void RTreeListView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //e.Effects
            Mouse.SetCursor(Cursors.Wait);
            e.UseDefaultCursors = false;
            e.Handled = true;

        }
    }
}
