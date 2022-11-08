using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core;
using NeoTrader.Resources.Theme;

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


        var neoBlackTheme = DevExpress.Xpf.Core.Theme.CreateTheme(new NeoBlackThemePalette(), DevExpress.Xpf.Core.Theme.VS2019Dark);
        DevExpress.Xpf.Core.Theme.RegisterTheme(neoBlackTheme);
        
        //ApplicationThemeHelper.ApplicationThemeName = DevExpress.Xpf.Core.Theme.VS2019Dark.Name;
        ApplicationThemeHelper.ApplicationThemeName = neoBlackTheme.Name;

    }
}
}