/********************************************************************
    created:	2022/2/9 17:21:26
    author:		rush
    email:		yacper@gmail.com	
	
    purpose:    用于一般命令类型的viewmodel
    modifiers:	
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Xpf.Bars;
using System.Windows.Media;
using System.Windows;
using DevExpress.Mvvm;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;

namespace NeoTrader;

public class CommandViewModel : ViewModelBase
{
    public override string ToString() => $"Command:{DisplayName}";

    public ICommand                               Command     { get;               set; }                                                          // 对应command
    public ObservableCollection<CommandViewModel> Commands    { get;               set; }                                                          // 子命令项 (用于menu的子命令)
    public bool                                   IsSeparator { get;               set; }                                                          // 是否只是一个seperator
    public BarItemDisplayMode                     DisplayMode { get;               set; }                                                          // 显示模式，纯文字还是带icon
    public bool                                   IsComboBox  { get;               set; }                                                          // 是否作为combox
    public bool                                   IsCheckBox  { get;               set; }                                                          // 是否作为checkbox
    public bool                                   IsChecked   { get => _IsChecked; set => SetProperty(ref _IsChecked, value, nameof(IsChecked)); } // 是否check
    public bool                                   IsEnabled   { get;               set; }                                                          //是否启用
    public KeyGesture                             KeyGesture  { get;               set; }                                                          // 对应快捷键


    public object Owner { get; private set; } // 如果有owner workspace, 关联开启workspace


    //public  bool                                   IsConnectBox   { get; set; } // 连接
    //public  ICommand                               SettingCommand { get; set; } // 作为连接Bar设置的command	
    //public  BarItemAlignment                       ItemAlignment  { get; set; } // 作为statusBar时使用
    //public  bool                                   IsStatusBar    {get;  set; } // 是否作为statusBar

    //   public bool       IsSubItem   { get; set; } // 是否作为子命令
    //   public bool       CanDelete   { get; set; } // 是否可以移除


    public CommandViewModel() { IsEnabled = true; }

    public CommandViewModel(string displayName, ObservableCollection<CommandViewModel> subCommands) : this(displayName, null, null,
                                                                                                           subCommands)
    {
    }

    public CommandViewModel(string displayName, ICommand command = null) : this(displayName, null, command, null) { }

    public CommandViewModel(object owner, ICommand command) : this(string.Empty, owner, command) { }

    private CommandViewModel(string            displayName, object owner = null, ICommand command = null,
        ObservableCollection<CommandViewModel> subCommands = null)
    {
        IsEnabled = true;
        Owner     = owner;
        if (Owner != null)
        {
            //DisplayName = Owner.DisplayName;
            //Glyph       = Owner.Glyph;
        }
        else { DisplayName = displayName; }

        Command  = command;
        Commands = subCommands;
    }

    private bool _IsChecked;
}