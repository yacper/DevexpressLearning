// created: 2023/04/10 21:45
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.PropertyGrid;

namespace VisualStudioDockingCustomSerialization.ViewModels;


abstract public class PanelWorkspaceViewModel : WorkspaceViewModel, IMVVMDockingProperties
{

    protected PanelWorkspaceViewModel() { _targetName = WorkspaceName; }

    abstract protected string     WorkspaceName { get; }
    string IMVVMDockingProperties.TargetName    { get { return _targetName; } set { _targetName = value; } }

    public virtual void OpenItemByPath(string path)
    {

    }

    string _targetName;
}

