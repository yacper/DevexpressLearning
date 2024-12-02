根据Demo的`MVVM Serialization`例子，`DockLayoutManager`只支持序列化或反序列化layout位置信息，本身不负责也不支持ViewModel的序列化和反序列化。

所以只能分2份数据，一份储存layout对应的viewmodel信息，一份layout信息。


这个例子，就实现这种思路。


另外，通过
    <DataTemplate DataType="{x:Type viewModels:OutputViewModel}">
        <!--使用自定义控件，并且可以根据viewModel自动创建view-->
        <views:OutputView DataContext="{Binding}"/>
    </DataTemplate>
这种方式，可以根据ViewModel自动创建View，不需要在代码中手动创建View。


另外，使用Autofac代替devexpress的mvvm。

