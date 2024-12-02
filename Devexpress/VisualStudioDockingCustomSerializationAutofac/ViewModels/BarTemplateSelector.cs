using System.Windows;
using System.Windows.Controls;

namespace VisualStudioDockingCustomSerializationAutofac.ViewModels;

public class BarItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate BarCheckItemTemplate     { get; set; }
    public DataTemplate BarItemTemplate          { get; set; }
    public DataTemplate BarSubItemTemplate       { get; set; }
    public DataTemplate BarItemSeparatorTemplate { get; set; }
    public DataTemplate BarComboBoxTemplate      { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        CommandViewModel commandViewModel = item as CommandViewModel;
        if (commandViewModel != null)
        {
            DataTemplate template = null;
            if (commandViewModel.Owner != null)
                template = BarCheckItemTemplate;
            if (commandViewModel.IsSubItem)
                template = BarSubItemTemplate;
            if (commandViewModel.IsSeparator)
                template = BarItemSeparatorTemplate;
            if (commandViewModel.IsComboBox)
                template = BarComboBoxTemplate;
            return template == null ? BarItemTemplate : template;
        }

        return base.SelectTemplate(item, container);
    }
}

