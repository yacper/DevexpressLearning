using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace NeoControls.View
{
    /// <summary>
    /// ThemeTest.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeTest : ThemedWindow
    {
        public ThemeTest()
        {
            var custompalette = new ThemePalette("1");
            custompalette.SetColor("Bull", Colors.Blue);
            var customtheme = Theme.CreateTheme(custompalette, Theme.Office2016ColorfulSE);
            Theme.RegisterTheme(customtheme);

            ApplicationThemeHelper.ApplicationThemeName = customtheme.Name;


            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var custompalette = new ThemePalette("CustomPalette");
            custompalette.SetColor("Foreground", (Color)ColorConverter.ConvertFromString("#FFFF7200"));
            custompalette.SetColor("Backstage.Focused", Colors.White);
            custompalette.SetColor("Bull", Colors.Red);
            var customtheme = Theme.CreateTheme(custompalette, Theme.Office2016ColorfulSE);
            Theme.RegisterTheme(customtheme);

            ApplicationThemeHelper.ApplicationThemeName = customtheme.Name;
        }
    }
}
