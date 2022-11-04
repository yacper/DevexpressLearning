using DevExpress.Mvvm;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using NeoControls;
using NeoTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NeoTrader.UI.Controls
{
    public enum TLVDragDropLimtEnum
    {
        None,                                  // 可以随意拖拽
        TableView,                             // 類似TalbeView拖拽 只能有一层，相同层之前不能添加為子結點
        ParentNoAppend                         // 相同的父节点可以拖拽，并不改变拖拽元素的层级
    }

    public class RTreeListView : TreeListView
    {
        public static DependencyProperty DropLimtEnumProperty = DependencyProperty.Register(nameof(DropLimtEnum), typeof(TLVDragDropLimtEnum), typeof(RTreeListView),
            new PropertyMetadata(TLVDragDropLimtEnum.None, (d, e) => 
            {
                var rTlv = d as RTreeListView;
                rTlv!.DropLimtEnum = (TLVDragDropLimtEnum)e.NewValue;
            }));

        public TLVDragDropLimtEnum DropLimtEnum 
        {
            get => (TLVDragDropLimtEnum)GetValue(DropLimtEnumProperty);
            set => SetValue(DropLimtEnumProperty, value);
        }

        public static DependencyProperty ChildToolCommandsTemplateProperty = DependencyProperty.Register(nameof(ChildToolCommandsTemplate), typeof(ObservableCollection<CommandVm>), typeof(RTreeListView));
        public ObservableCollection<CommandVm> ChildToolCommandsTemplate
        {
            get => (ObservableCollection<CommandVm>)GetValue(ChildToolCommandsTemplateProperty);
            set => SetValue(ChildToolCommandsTemplateProperty, value);
        }

        public ICommand RMouseDoubleClickCommand { get; private set; }

        public RTreeListView() : base()
        {
            AllowEditing = false;
            AllowDragDrop = true;
            ShowIndicator = false;
            ShowRootIndent = false;

            DragRecordOver += RTreeListView_DragRecordOver;
            StartRecordDrag += RTreeListView_StartRecordDrag;

            RMouseDoubleClickCommand = new DelegateCommand<TreeListRowData>(tlr => 
            {
                if (tlr == null)
                    return;

                tlr.Node.IsExpanded = !tlr.IsExpanded;
            });

            RowStyle = new Style(typeof(RowControl));
            RowStyle.Setters.Add(new Setter()
            {
                Property = RowControl.TemplateProperty,
                Value = App.Current.FindResource("RDataGrid_RowControlTemplate")
            });

        }

        #region Drag
        private void RTreeListView_StartRecordDrag(object sender, StartRecordDragEventArgs e)
        {
            dragNodes = new List<TreeListNode>();
            int[] rowHandles = GetSelectedRowHandles();
            foreach (var idx in rowHandles)
            {
                dragNodes.Add(GetRowControlNodeByHandle(idx));
            }
        }

        private void RTreeListView_DragRecordOver(object sender, DragRecordOverEventArgs e)   // TODO： IsFromOutside = true 情况
        {
            var overNode = GetRowControlNodeByHandle(e.TargetRowHandle);
            if (overNode == null)
            {
                e.Effects = DragDropEffects.None;
                return;
            }

            if(DropLimtEnum == TLVDragDropLimtEnum.ParentNoAppend)           // 只能在相同的父节点中拖拽
            {
                int level = dragNodes[0].ActualLevel;
                var pNode = dragNodes[0].ParentNode;
                foreach (var node in dragNodes)
                {
                    if(node.ActualLevel != level || node.ParentNode != pNode)           // 基本数据都不符合拖拽条件
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                }

                if(overNode.ActualLevel != level || overNode.ParentNode != pNode)       // 放置的元素必须是相同的父节点
                {
                    e.Effects = DragDropEffects.None;
                    return;
                }

                if(e.DropPosition == DropPosition.Append)                           // 兄弟节点直接不能是 Append 模式
                {
                    e.Effects = DragDropEffects.None;
                    return;
                }
            }

            if(DropLimtEnum == TLVDragDropLimtEnum.TableView)
            {
                System.Diagnostics.Debug.WriteLine(e.DropPosition);
                if (e.DropPosition == DropPosition.Append || e.DropPosition == DropPosition.Inside)                          
                {
                    e.Effects = DragDropEffects.None;
                    return;
                }
            }

        }
        #endregion

        private TreeListNode GetRowControlNodeByHandle(int idx)
        {
            var rc = GetRowElementByRowHandle(idx) as RowControl;
            if (rc == null)
                return null; 
            return (rc!.DataContext as TreeListRowData)!.Node;
        }


        private List<TreeListNode> dragNodes;
    }
}
