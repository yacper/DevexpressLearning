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

public class CommandVm : ViewModelBase
{
    public override string ToString() => $"Command:{DisplayName}";

    public ICommand                        Command  { get; set; } // 对应command(可以为空，那么只有点开子命令功能)
    public ObservableCollection<CommandVm> Commands { get; set; } // 子命令项 (用于menu的子命令)

    public BarItemDisplayMode DisplayMode { get; set; } // 显示模式，纯文字还是带icon
    public bool               IsCheckBox  { get; set; } // 是否作为checkbox
    public bool               IsSeparator { get; set; } // 是否只是一个seperator
    public KeyGesture KeyGesture { get; set; } // 对应快捷键


#region State状态，不应被用户设置
    public virtual bool       IsSubItem  { get; set; } // 是否被移到submenu中
    public virtual bool       IsChecked  { get; set; }
    public virtual bool       IsEnabled  { get; set; } = true;    //是否启用
    public virtual Visibility Visibility { get; set; } = Visibility.Visible; //是否启用
#endregion

    public object Owner { get; set; } // 如果有owner workspace, 关联开启workspace


    public CommandVm() { }

    public CommandVm(string displayName, ObservableCollection<CommandVm> subCommands) : this(displayName, null,
                                                                                             subCommands)
    {
    }

    public CommandVm(string displayName, ICommand command = null) : this(displayName, command, null) { }


    private CommandVm(string            displayName, ICommand command = null,
        ObservableCollection<CommandVm> subCommands = null)
    {
        DisplayName = displayName;

        Command  = command;
        Commands = subCommands;
    }
}