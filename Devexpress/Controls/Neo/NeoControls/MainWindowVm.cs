// created: 2022/10/27 20:12
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Bars;
using NeoTrader;
using NeoTrader.UI.ViewModels;

namespace NeoControls;

public class MainWindowVm : VmBase
{
    public MainWindowVm()
    {
        DisplayName = "hello";


        ToolButtonVm =new CommandVm("null", new DelegateCommand(() => { Debug.WriteLine(DisplayName); }))
        {
            Glyph = Images.Account,
            DisplayMode = BarItemDisplayMode.ContentAndGlyph
        }
                                                              .WithPropertyBinding(p => DisplayName, this, n => DisplayName).
                                                              WithPropertyBinding(p => BadgeContent, this, n => BadgeContent)
                                                        ;

        this.PropertyChanged += (s, e) => { Debug.WriteLine(e.PropertyName + " Changed"); };
    }


    public virtual CommandVm ToolButtonVm { get; set; }

    public virtual CommandVm ContentVm { get; set; } =
        new("ContentVm", new DelegateCommand(() => { Debug.WriteLine("ContentVm clicked"); }))
        {
            Glyph = Images.Account, DisplayMode = BarItemDisplayMode.Content
        };

    public virtual CommandVm SeperatorVm { get; set; } =
        new("SeperatorVm")
        {
            IsSeparator = true,
            Glyph       = Images.Account, DisplayMode = BarItemDisplayMode.Content
        };


    public virtual CommandVm ContentAndGlyphVm { get; set; } =
        new("ContentAndGlyphVm", new DelegateCommand(() => { Debug.WriteLine("ContentAndGlyphVm clicked"); })) { Glyph = Images.Account, DisplayMode = BarItemDisplayMode.ContentAndGlyph };

    public virtual CommandVm ContentAndGlyphAndStateVm { get; set; } =
        new("ContentAndGlyphAndStateVm", new DelegateCommand(() => { Debug.WriteLine("ContentAndGlyphAndStateVm clicked"); }))
            { Glyph = Images.Account, StateImg = Images.ConnectedStatus, DisplayMode = BarItemDisplayMode.ContentAndGlyph };

    public virtual CommandVm ContentAndGlyphAndStateAndBadgeVm { get; set; } =
        new("ContentAndGlyphAndStateAndBadgeVm", new DelegateCommand(() => { Debug.WriteLine("ContentAndGlyphAndStateAndBadgeVm clicked"); }))
            { Glyph = Images.Account, StateImg = Images.ConnectedStatus, BadgeContent = "D", DisplayMode = BarItemDisplayMode.ContentAndGlyph };


    public KeyGesture KeyGesture { get; set; } = new KeyGesture(Key.F, ModifierKeys.Alt);

    public virtual ObservableCollection<CommandVm> ToolBarsVms { get; set; } = new ObservableCollection<CommandVm>() 
    {
        new CommandVm("ContentAndGlyph", new DelegateCommand(() => { }))
        {Glyph = Images.Account, DisplayMode=BarItemDisplayMode.ContentAndGlyph, KeyGesture=new KeyGesture(Key.A, ModifierKeys.Control), Commands = new ObservableCollection<CommandVm>()
        {
            new CommandVm("Content", new DelegateCommand(() => { }))
            { DisplayMode=BarItemDisplayMode.Content, KeyGesture=new KeyGesture(Key.A, ModifierKeys.Control)},
            new CommandVm("ContentAndGlyph", new DelegateCommand(() => { }))
            { DisplayMode=BarItemDisplayMode.ContentAndGlyph, Glyph=Images.Trading},
            new CommandVm("ContentAndGlyphAndState", new DelegateCommand(() => { }))
            { DisplayMode=BarItemDisplayMode.ContentAndGlyph,Glyph=Images.Trading, StateImg = Images.ConnectingStatus, KeyGesture=new KeyGesture(Key.A, ModifierKeys.Control)},
            new CommandVm("ContentAndGlyphAndStateAddBage", new DelegateCommand(() => { }))
            { DisplayMode=BarItemDisplayMode.ContentAndGlyph,Glyph=Images.Trading, StateImg = Images.ConnectingStatus, BadgeContent="F",KeyGesture=new KeyGesture(Key.A, ModifierKeys.Control)},
        } },
        new CommandVm("Content", new DelegateCommand(() => {  }))
        {DisplayMode=BarItemDisplayMode.Content},
        new CommandVm("Glyph", new DelegateCommand(() => { }))
        {Glyph = Images.Account},
        new CommandVm("ContentAndGlyphAndState", new DelegateCommand(() => { }))
        {DisplayMode=BarItemDisplayMode.ContentAndGlyph,Glyph=Images.Trading, StateImg = Images.ConnectingStatus},
        new CommandVm("ContentAndGlyphAndStateAndBadge", new DelegateCommand(() => { }))
        {DisplayMode=BarItemDisplayMode.ContentAndGlyph,Glyph=Images.Trading, StateImg = Images.ConnectingStatus, BadgeContent="F",KeyGesture=new KeyGesture(Key.A, ModifierKeys.Control)},
    };
}