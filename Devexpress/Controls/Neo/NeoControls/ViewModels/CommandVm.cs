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
using Illusion.Utility;
using System.Linq.Expressions;
using Force.DeepCloner;

namespace NeoTrader;

public class CommandVm : ViewModelBase, ICloneable
{
    public override string ToString() => $"Command:{DisplayName}";

    public ICommand                        Command  { get; set; } // 对应command(可以为空，那么只有点开子命令功能)
    public ObservableCollection<CommandVm> Commands { get; set; } // 子命令项 (用于menu的子命令)

    public BarItemDisplayMode DisplayMode { get; set; } // 显示模式，纯文字还是带icon
    public bool               IsCheckBox  { get; set; } // 是否作为checkbox
    public bool               IsLink { get; set; }     // 是否作为 LinkBtn   
    public bool               IsSeparator { get; set; } // 是否只是一个seperator
    public KeyGesture         KeyGesture  { get; set; } // 对应快捷键
    public object             Tag { get; set; }    // 存储相关的数据 

     public  CommandVm WithPropertyBinding<TSource>( 
        Expression<Func<CommandVm, object>> targetExpression,
        TSource                             source, Expression<Func<TSource, object>> sourceExpression,
        BindingValueChangedHandler          targetChangedHandler = null)
    {
        //todo: remove binding
        var binding = BindingEngine.SetPropertyBinding(this, targetExpression, source, sourceExpression);
        if (targetChangedHandler != null)
            binding.SetTargetChanged(targetChangedHandler);

        return this;
    }

    public object Clone()
    {
        return this.MemberwiseClone();        // 这里 deep Copy 好像有问题
    }

    #region State状态，不应被用户设置

    public virtual bool       IsSubItem  { get; set; } // 是否被移到submenu中
    public virtual bool       IsChecked  { get=>GetProperty(()=>IsChecked); set=>SetProperty(()=>IsChecked, value); }
    public virtual bool       IsEnabled  { get=>GetProperty(()=>IsEnabled); set=>SetProperty(()=>IsEnabled, value); }               //是否启用
    public virtual Visibility Visibility { get=>GetProperty(()=>Visibility); set=>SetProperty(()=>Visibility, value); }  //是否启用

#endregion

    public object Owner { get; set; } // 如果有owner workspace, 关联开启workspace


    public CommandVm()
    {
        IsEnabled  = true;
        Visibility = Visibility.Visible;
    }

    public CommandVm(string displayName) : this(displayName, null, null)
    {
    }

    public CommandVm(string displayName, ObservableCollection<CommandVm> subCommands) : this(displayName, null,
                                                                                             subCommands)
    {
    }

    public CommandVm(string displayName, ICommand command = null) : this(displayName, command, null) { }


    private CommandVm(string            displayName, ICommand command = null,
        ObservableCollection<CommandVm> subCommands = null)
        :this()
    {
        DisplayName = displayName;

        Command  = command;
        Commands = subCommands;
    }
}

//public static class CommandVmEx
//{
//     public static CommandVm WithPropertyBinding<TSource>(this CommandVm vm, 
//        Expression<Func<CommandVm, object>> targetExpression,
//        TSource                             source, Expression<Func<TSource, object>> sourceExpression,
//        BindingValueChangedHandler          targetChangedHandler = null)
//    {
//        //todo: remove binding
//        var binding = BindingEngine.SetPropertyBinding(vm, targetExpression, source, sourceExpression);
//        if (targetChangedHandler != null)
//            binding.SetTargetChanged(targetChangedHandler);

//        return vm;
//    }


//}
