using System.Text.RegularExpressions;
using System.Windows.Media;
using DevExpress.Mvvm;

namespace VisualStudioDockingCustomSerializationAutofac.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    // 序列化到layout时，<property name="Name">ErrorData</property>， Name即为BindableName
    public virtual string      BindableName { get { return GetBindableName(DisplayName); } } 

    public virtual string      DisplayName  { get; protected set; }
    public virtual ImageSource Glyph        { get; set; }

    string GetBindableName(string name) { return "_" + Regex.Replace(name, @"\W", ""); }

#region IDisposable Members

    public            void Dispose()   { OnDispose(); }
    protected virtual void OnDispose() { }
#if DEBUG
    ~ViewModel()
    {
        string msg = string.Format("{0} ({1}) ({2}) Finalized", GetType().Name, DisplayName, GetHashCode());
        System.Diagnostics.Debug.WriteLine(msg);
    }
#endif

#endregion
}