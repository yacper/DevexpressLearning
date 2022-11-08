// created: 2022/10/27 11:02
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace NeoTrader.Resources.Theme;

public class NeoBlackThemePalette : ThemePalette
{
    public NeoBlackThemePalette() : base("NeoBlackTheme")
    {
        //SetColor("Foreground", (Color)ColorConverter.ConvertFromString("#FFF2F2F2"));
        SetColor("Bull", (Color)ColorConverter.ConvertFromString("#FFEF5350"));
        SetColor("Bear", (Color)ColorConverter.ConvertFromString("#FF26A69A"));
        //SetColor("SelectionBackground", (Color)ColorConverter.ConvertFromString("#FF000000"));
    }
}