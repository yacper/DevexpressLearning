﻿// created: 2022/10/27 20:12
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System.Diagnostics;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Bars;
using NeoTrader;
using ViewModelBase = NeoTrader.ViewModelBase;

namespace NeoControls;

public class MainWindowVm : ViewModelBase
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


    public virtual void ChangeDisplayName()
    {
        DisplayName  =  "World";
        BadgeContent += "1";
    }
}