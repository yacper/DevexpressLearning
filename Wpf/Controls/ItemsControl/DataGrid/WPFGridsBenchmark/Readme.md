https://www.grapecity.com/blogs/wpf-datagrid-performance-comparison


我的测试是与ObservableCollection绑定的DataGrid的基本实现,它每秒更新一次.它还包括可扩展的详细信息区域,以显示有关每行的更多信息.详细信息区域只是一个具有ItemsControl包装TextBlock(重复6次)的堆栈面板

https://stackoverflow.com/questions/49635047/wpf-datagrid-very-slow
ALL DataGrid's are horrendously slow (even the ones you pay for). You need to turn on virtualization. 
VirtualizingStackPanel.IsVirtualizing = true as well as VirtualizingStackPanel.VirtualizationMode = recycling. 
If you have a lot of columns, or start to do templates, it'll slow to a crawl again. Nothing you can do about that really. 
I've tried every DataGrid out there (syncfusion, infragistics, etc). They are all very slow.


启用UI虚拟化的两个附加属性：
1、ScrollViewer.CanContentScroll="True"
2、VirtualizingStackPanel.IsVirtualizing="True"



Wpf datagrid 在通过scrollbar scroll的时候，非常慢
Devexpress gridcontrol 性能非常好，scroll速度很快
