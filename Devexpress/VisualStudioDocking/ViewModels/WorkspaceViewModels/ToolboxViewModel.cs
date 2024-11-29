// created: 2024/11/29 12:21
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using DevExpress.Xpf.DemoBase.Helpers;

namespace VisualStudioDocking.ViewModels;

public class ToolboxViewModel : PanelWorkspaceViewModel
{
    public ToolboxViewModel()
    {
        DisplayName = "Toolbox";
        Glyph       = Images.Toolbox;
        Tag        = "Toolbox_Tag";
    }

    protected override string WorkspaceName { get { return "LeftHost"; } }
}
