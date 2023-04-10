// created: 2023/04/10 21:45
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using DevExpress.Xpf.Docking;

namespace VisualStudioDocking.ViewModels;

abstract public class PanelWorkspaceViewModel : WorkspaceViewModel, IMVVMDockingProperties
{
    string _targetName;

    protected PanelWorkspaceViewModel() { _targetName = WorkspaceName; }

    abstract protected string     WorkspaceName { get; }
    string IMVVMDockingProperties.TargetName    { get { return _targetName; } set { _targetName = value; } }

    public virtual void OpenItemByPath(string path)
    {

    }
}