using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using NeoTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NeoDataGrid.Control
{
    public class CombineDataItem : IRowTools            // 组合数据
    {
        #region RowTool
        public ObservableCollection<RowToolsViewMode> ToolVMs => RowTools.ToolVMs;

        public RowToolsViewMode SelectedToolVm 
        {
            get => RowTools.SelectedToolVm;
            set 
            {
                RowTools.SelectedToolVm = value;
            }
        }

        public bool ToolIsFixed => RowTools.ToolIsFixed;

        public Brush ToolsBgBrush => RowTools.ToolsBgBrush;
        #endregion

        public Object Source { get; set; }              // 真是的数据源
        public RowTools RowTools { get; set; }

        public CombineDataItem(object source, RowTools tools)
        {
            Source = source;
            RowTools = tools;
        }
    }

    public class RGridControl : GridControl
    {
        // Tools 数据
        public static DependencyProperty RowToolsProperty = DependencyProperty.Register("RowTools", typeof(IEnumerable<IRowTools>), typeof(RGridControl), 
            new PropertyMetadata(null, (d,e) => 
            {
                var rc = d as RGridControl;
                rc!.RowTools = (IEnumerable<IRowTools>)e.NewValue;
            }));

        // 原始数据
        public static DependencyProperty RItemSourceProperty = DependencyProperty.Register("RItemSource", typeof(object), typeof(RGridControl), new PropertyMetadata(null, (d,e) => 
        {
            var rc = d as RGridControl;
            rc!.RItemSource = e.NewValue;
        }));

        public IEnumerable<IRowTools> RowTools 
        {
            get => (IEnumerable<IRowTools>)GetValue(RowToolsProperty);
            set 
            {
                SetValue(RowToolsProperty, value);
                GengenerateItems();
            }
        }

        public object RItemSource
        {
            get=>GetValue(RItemSourceProperty);
            set 
            { 
                SetValue(RItemSourceProperty, value);
                GengenerateItems();
            }
        }

        public RGridControl(): base()
        {
            GengenerateItems();
        }

        // 
        private void GengenerateItems()                                     // 首次创建调用
        {
            if (RowTools == null || RItemSource == null || (_Items != null && _Items.Count > 0))
                return;

            CheckAccess();
            _Items = new ObservableCollection<CombineDataItem>();

            int count = (RItemSource as ICollection)!.Count;
            for (int i = 0;i < (RItemSource as ICollection)!.Count; i++)
                _Items.Add(new CombineDataItem((RItemSource as IList)![i]!, (RowTools)RowTools.Skip(i).First()));

            ItemsSource = _Items;

            _Items.CollectionChanged += _Items_CollectionChanged;
            (RItemSource as INotifyCollectionChanged)!.CollectionChanged += RItemSource_CollectionChanged;
        }   

        private void RItemSource_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)      // 原始数据变化 ====》 Items
        {
            _Items.CollectionChanged -= _Items_CollectionChanged;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                CheckAccess();
                int idx = e.NewStartingIndex;
                idx = idx == 0 ? 0 : idx - 1;
                _Items.Insert(e.NewStartingIndex, new CombineDataItem((RItemSource as IList)![e.NewStartingIndex]!, (RowTools)RowTools.Skip(idx).First()));                
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
                _Items.RemoveAt(e.OldStartingIndex);
            
            _Items.CollectionChanged += _Items_CollectionChanged;
        }

        private void _Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)           //  Items ====》 原始数据
        {
            (RItemSource as INotifyCollectionChanged)!.CollectionChanged -= RItemSource_CollectionChanged;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var cItem = e.NewItems![0] as CombineDataItem;
                (RowTools as IList)!.Insert(e.OldStartingIndex, cItem!.RowTools);
                (RItemSource as IList)!.Insert(e.OldStartingIndex, cItem!.Source);                
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                (RowTools as IList)!.RemoveAt(e.OldStartingIndex);
                (RItemSource as IList)!.RemoveAt(e.OldStartingIndex);
            }

            (RItemSource as INotifyCollectionChanged)!.CollectionChanged += RItemSource_CollectionChanged;
        }

        private void RowToolsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GengenerateItems();
        }

        private void RItemSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GengenerateItems();
        }

        private void CheckAccess()
        {
            // TODO: 优化
            if (!(RItemSource is IEnumerable))
                throw new Exception("RItemSource 必须是集合");
            if ((RItemSource as IList)!.Count > RowTools.Count())
                throw new InvalidOperationException($"RItemSource: {(RItemSource as IList)!.Count} 大于 RowTools: {RowTools.Count()}");
        } 

        private ObservableCollection<CombineDataItem> _Items;
    }
}
