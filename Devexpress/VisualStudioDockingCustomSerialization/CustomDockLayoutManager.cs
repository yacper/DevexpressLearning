// created: 2024/11/29 14:57
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:     通过CreateSerializationController可以创建自定义的dockitem，但总体用处不大，除非是做更深层次的定制
// modifiers:

using DevExpress.Utils.Serializing.Helpers;
using DevExpress.Xpf.Docking;

namespace VisualStudioDockingCustomSerialization;

//class CustomDockLayoutManager : DockLayoutManager
//{
//    protected override ISerializationController CreateSerializationController() { return new CustomSerializationController(this); }
//    protected internal virtual toolbox CreateCustomPanel() { return new MyCustomPanel(); }
//}

//class CustomSerializationController : SerializationController
//{
//    public CustomSerializationController(DockLayoutManager container)
//        : base(container)
//    {
//    }

//    protected override BaseLayoutItem CreateItemByType(XtraPropertyInfo info, string typeStr)
//    {
//        if (typeStr == typeof(MyCustomPanel).Name)
//            return ((CustomDockLayoutManager)Container).CreateCustomPanel();

//        return base.CreateItemByType(info, typeStr);
//    }
//}