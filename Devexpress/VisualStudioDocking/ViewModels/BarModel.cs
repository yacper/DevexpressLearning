using System;
using System.Collections.Generic;

namespace VisualStudioDocking.ViewModels
{
public class BarModel : ViewModel
{
    public BarModel(string displayName)
    {
        DisplayName = displayName;
    }

    public List<CommandViewModel> Commands   { get; set; }
    public bool                   IsMainMenu { get; set; }
}
}