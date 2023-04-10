// created: 2023/04/10 21:47
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System;
using DevExpress.Mvvm.DataAnnotations;

namespace VisualStudioDocking.ViewModels;

public abstract class WorkspaceViewModel : ViewModel
{
    public override string BindableName { get { return Data; } }

    protected WorkspaceViewModel() { IsClosed = true; }

    public event EventHandler RequestClose;

    public virtual bool IsActive { get; set; }

    [BindableProperty(OnPropertyChangedMethodName = "OnIsClosedChanged")]
    public virtual bool IsClosed { get; set; }

    public virtual bool IsOpened { get; set; }


    //public virtual string SerializationInfo { get; set; }
    public virtual string Data { get; set; }
    public virtual string Id   { get; set; } = new Guid().ToString();

    public void Close()
    {
        EventHandler handler = RequestClose;
        if (handler != null)
            handler(this, EventArgs.Empty);
    }

    protected virtual void OnIsClosedChanged() { IsOpened = !IsClosed; }
}