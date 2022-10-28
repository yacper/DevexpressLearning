using System;
using System.IO;
using System.Text;
using System.Windows;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using Microsoft.Win32;

namespace VisualStudioDocking
{
public interface IDockingSerializationDialogService
{
    void SaveLayout();
    void LoadLayout();
}

public class DockingSerializationDialogService : ServiceBase, IDockingSerializationDialogService
{
    const  string            filter = "Configuration (*.xml)|*.xml|All files (*.*)|*.*";
    public DockLayoutManager DockLayoutManager { get; set; }

    public void LoadLayout()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = filter };
        var            openResult     = openFileDialog.ShowDialog();
        if (openResult.HasValue && openResult.Value) { DockLayoutManager.RestoreLayoutFromXml(openFileDialog.FileName); }
    }

    public void SaveLayout()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = filter };
        var            saveResult     = saveFileDialog.ShowDialog();
        if (saveResult.HasValue && saveResult.Value)
        {
            DockLayoutManager.AddHandler(DXSerializer.CustomGetSerializablePropertiesEvent, new CustomGetSerializablePropertiesEventHandler(CustomGetSerializablePropertiesHandler));

            //using (var stream = new MemoryStream())
            //{
            //    DockLayoutManager.SaveLayoutToStream(stream);
            //    var str = Encoding.UTF8.GetString(stream.ToArray());
            //    //if (Convert.ToInt32(str[0]) == Convert.ToInt32(0x3F))
            //    str = str.Substring(1, str.Length - 1);
            //    //return str; // 转化成utf8后，第一位总是0x3f(?),把他去掉

            //}

            DockLayoutManager.SaveLayoutToXml(saveFileDialog.FileName);
        }
    }

    void CustomGetSerializablePropertiesHandler(object sender, CustomGetSerializablePropertiesEventArgs e) { e.SetPropertySerializable(FrameworkElement.TagProperty, new DXSerializable() { }); }

    protected override void OnAttached()
    {
        base.OnAttached();
        DockLayoutManager = AssociatedObject as DockLayoutManager;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        DockLayoutManager = null;
    }
}
}