using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VisualStudioDockingCustomSerializationAutofac.ViewModels;

public class BarModel : ViewModel
{
    public BarModel(string displayName)
    {
        DisplayName = displayName;
    }

    public List<CommandViewModel> Commands   { get; set; }
    public bool                   IsMainMenu { get; set; }
}

public class BarTemplateSelector : DataTemplateSelector
{
    public DataTemplate MainMenuTemplate { get; set; }
    public DataTemplate ToolbarTemplate  { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        BarModel barModel = item as BarModel;
        if (barModel != null) { return barModel.IsMainMenu ? MainMenuTemplate : ToolbarTemplate; }

        return base.SelectTemplate(item, container);
    }
}

