using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core;

namespace NeoControls
{
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        
        ApplicationThemeHelper.ApplicationThemeName = DevExpress.Xpf.Core.Theme.VS2019Dark.Name;
    }
}
}