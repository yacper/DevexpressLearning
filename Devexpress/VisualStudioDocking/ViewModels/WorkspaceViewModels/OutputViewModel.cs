﻿// created: 2024/11/29 12:26
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

namespace VisualStudioDocking.ViewModels;

public class OutputViewModel : PanelWorkspaceViewModel
{
    public OutputViewModel()
    {
        DisplayName = "Output";
        Glyph       = Images.Output;
        Text = @"1>------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug Any CPU ------
1>  DockingDemo -> C:\VisualStudioInspiredUIDemo.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========";
    }

    public             string Text          { get; private set; }
    protected override string WorkspaceName { get { return "BottomHost"; } }
}
