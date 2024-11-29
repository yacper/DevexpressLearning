// created: 2023/04/10 21:47
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System;
using DevExpress.Mvvm.DataAnnotations;

namespace VisualStudioDockingCustomSerialization.ViewModels;

public class WorkspaceViewModelData
{
    public string ViewModelType { get; set; }

    public string Data { get; set; }
}



public abstract class WorkspaceViewModel : ViewModel
{
    public WorkspaceViewModelData ViewModelData=> new WorkspaceViewModelData
    {
        ViewModelType = GetType().FullName.Split('_')[0],
        Data          = Data
    };

    // 序列化数据
    public virtual string Data { get; }

    // 反序列化数据
    public virtual void OnRestore(string data)
    {

    }



    protected WorkspaceViewModel() { IsClosed = true; }

    public event EventHandler RequestClose;

    public virtual bool IsActive { get; set; }

    [BindableProperty(OnPropertyChangedMethodName = "OnIsClosedChanged")]
    public virtual bool IsClosed { get; set; }

    public virtual bool IsOpened { get; set; }


    public void Close()
    {
        EventHandler handler = RequestClose;
        if (handler != null)
            handler(this, EventArgs.Empty);
    }

    protected virtual void OnIsClosedChanged() { IsOpened = !IsClosed; }
}