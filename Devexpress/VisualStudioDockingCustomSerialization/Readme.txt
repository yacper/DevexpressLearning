根据Demo的`MVVM Serialization`例子，`DockLayoutManager`只支持序列化或反序列化layout位置信息，本身不负责也不支持ViewModel的序列化和反序列化。

所以只能分2份数据，一份储存layout对应的viewmodel信息，一份layout信息。


这个例子，就实现这种思路。
