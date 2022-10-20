using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NeoDataGrid.Control
{
    public enum TLVDragDropLimtEnum
    {
        None,                                  // 可以随意拖拽
        ParentNoAppend                         // 相同的父节点可以拖拽，并不改变拖拽元素的层级
    }
    public class RTreeListView : TreeListView
    {
        public static DependencyProperty DropLimtEnumProperty = DependencyProperty.Register("DropLimtEnum", typeof(TLVDragDropLimtEnum), typeof(RTreeListView),
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

        public RTreeListView() : base()
        {
            DragRecordOver += RTreeListView_DragRecordOver;
            StartRecordDrag += RTreeListView_StartRecordDrag;
        }

        #region Drag
        private void RTreeListView_StartRecordDrag(object? sender, StartRecordDragEventArgs e)
        {
            dragNodes = new List<TreeListNode>();
            int[] rowHandles = GetSelectedRowHandles();
            foreach (var idx in rowHandles)
            {
                dragNodes.Add(GetRowControlNodeByHandle(idx));
            }
        }

        private void RTreeListView_DragRecordOver(object? sender, DragRecordOverEventArgs e)   // TODO： IsFromOutside = true 情况
        {
            var overNode = GetRowControlNodeByHandle(e.TargetRowHandle);
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
        }
        #endregion

        private TreeListNode GetRowControlNodeByHandle(int idx)
        {
            var rc = GetRowElementByRowHandle(idx) as RowControl;            
            return (rc!.DataContext as TreeListRowData)!.Node;
        }

        private List<TreeListNode> dragNodes;
    }
}
