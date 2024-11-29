// created: 2024/11/29 12:30
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System.Windows.Media;

namespace VisualStudioDocking.ViewModels;

public class ErrorListViewModel : PanelWorkspaceViewModel
{
    public ErrorListViewModel()
    {
        DisplayName = "Error List";
        Glyph       = Images.TaskList;
        Error       = Images.Error;
        Warning     = Images.Warning;
        Info        = Images.Info;
        Tag        = "ErrorList_Tag";
    }

    public             ImageSource Error         { get; set; }
    public             ImageSource Info          { get; set; }
    public             ImageSource Warning       { get; set; }
    protected override string      WorkspaceName { get { return "BottomHost"; } }
}
