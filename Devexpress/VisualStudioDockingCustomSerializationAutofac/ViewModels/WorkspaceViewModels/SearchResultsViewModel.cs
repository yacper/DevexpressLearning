// created: 2024/11/29 12:30
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

namespace VisualStudioDockingCustomSerializationAutofac.ViewModels;

public class SearchResultsViewModel : PanelWorkspaceViewModel
{
    public SearchResultsViewModel()
    {
        DisplayName = "Search Results";
        Glyph       = Images.FindInFilesWindow;
        Text        = @"Matching lines: 0    Matching files: 0    Total files searched: 61";
    }

    public             string Text          { get; private set; }
    protected override string WorkspaceName { get { return "BottomHost"; } }
}
