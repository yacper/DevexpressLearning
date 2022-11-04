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
using DevExpress.Diagram.Core.Native.Generation;
using DevExpress.Mvvm.Native;
using Force.DeepCloner;
using System.Reflection;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace NeoTrader.UI.ViewModels;

public class CommandVm : VmBase
{
    public override string ToString() => $"Command:{DisplayName}";

    public virtual object      Owner  { get=>GetProperty(()=>Owner); set=>SetProperty(()=> Owner, value); }

    public CommandVm SetOwner(object newOwner)
    {
        Owner = newOwner;
        Bindings.ForEach(b => { b.Reset(); });

        Commands?.ForEach(p => p.SetOwner(newOwner));

        return this;
    }

    public CommandVm Clone(object owner)
    {
        CommandVm o = new(DisplayName, Command)
        {
            Owner = owner,
            Glyph = Glyph,
            StateImg = StateImg,
            BadgeContent = BadgeContent,

            Commands = Commands?.Select(p=>p.Clone(owner)).ToObservableCollection(),

            DisplayMode = DisplayMode,
            GlyphAlignment = GlyphAlignment,
            Alignment = Alignment,
            IsSubItem = IsSubItem,
            IsCheckBox = IsChecked,
            IsChecked = IsChecked,
            IsLink = IsLink,
            IsSeparator = IsSeparator,
            KeyGesture = KeyGesture,
            ShowKeyGesture = ShowKeyGesture,
            Tag = Tag
        };

        o.Bindings = this.Bindings.Select(p => p.Clone(o)).ToList();
        o.Bindings.ForEach(p=>p.Apply());
        return o;
    }

    public ICommand                        Command  { get; set; } // 对应command(可以为空，那么只有点开子命令功能)
    public ObservableCollection<CommandVm> Commands { get; set; } // 子命令项 (用于menu的子命令)

    public BarItemDisplayMode DisplayMode    { get; set; } = BarItemDisplayMode.ContentAndGlyph; // 显示模式，纯文字还是带icon
    public Dock               GlyphAlignment { get; set; } = Dock.Left; // glyph alignment
    public BarItemAlignment   Alignment      { get; set; } = BarItemAlignment.Near;              // Alignment
    public bool               IsSubItem      { get; set; } = false;                              // 有子命令
    public bool               IsCheckBox     { get; set; }                                       // 是否作为checkbox
    public bool               IsLink         { get; set; }                                       // 是否作为 LinkBtn   
    public bool               IsSeparator    { get; set; }                                       // 是否只是一个seperator
    public KeyGesture         KeyGesture     { get; set; }                                       // 对应快捷键
    public bool               ShowKeyGesture { get; set; } = true;
    public object             Tag            { get; set; } // 存储相关的数据 


     public  CommandVm WithPropertyBinding<TSource>( 
        Expression<Func<CommandVm, object>> targetExpression,
        TSource                             source,
        Expression<Func<TSource, object>> sourceExpression,
        BindingValueChangedHandler          targetChangedHandler = null)
    {
        var binding = new CommandVmPropertyBinding<TSource>()
        {
            Target = this,
            TargetExpression     = targetExpression,
            Source               = source,
            SourceExpression     = sourceExpression,
            TargetChangedHandler = targetChangedHandler
        };
        binding.Apply();
        Bindings.Add(binding);

        return this;
    }
    public  CommandVm WithPropertyBinding( 
        Expression<Func<CommandVm, object>> targetExpression,
        Expression<Func<CommandVm, object>> sourceExpression,
        BindingValueChangedHandler          targetChangedHandler = null)
    {
        var binding = new CommandVmPropertyBinding<CommandVm>()
        {
            Target = this,
            TargetExpression     = targetExpression,
            Source               = this,
            SourceExpression     = sourceExpression,
            TargetChangedHandler = targetChangedHandler
        };
        binding.Apply();
        Bindings.Add(binding);


        return this;
    }
    public CommandVm WithProperty<T>(Expression<Func<CommandVm, object>> memberLambda, T value)
    {
        var memberSelectorExpression = memberLambda.Body as MemberExpression;
        if (memberSelectorExpression != null)
        {
            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
                property.SetValue(this, value, null);
            }
        }

        return this;
    }


#region State状态，不应被用户设置
    public virtual bool       IsChecked  { get=>GetProperty(()=>IsChecked); set=>SetProperty(()=>IsChecked, value); }
    public virtual bool       IsEnabled  { get=>GetProperty(()=>IsEnabled); set=>SetProperty(()=>IsEnabled, value); }               //是否启用
    public virtual Visibility Visibility { get=>GetProperty(()=>Visibility); set=>SetProperty(()=>Visibility, value); }  //是否启用
#endregion


    public CommandVm()
    {
        IsEnabled  = true;
        Visibility = Visibility.Visible;
    }

    protected override void OnDispose()
    {
        Bindings.ForEach(p => BindingEngine.ClearBinding(p)); 
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

    protected List<CommandVmPropertyBinding> Bindings = new();
}

public class CommandVmPropertyBinding
{
    public CommandVm                                   Target               { get; set; }
    public Expression<Func<CommandVm, object>> TargetExpression     { get; set; }
    public object                                   Source               { get; set; }
    public Expression<Func<object, object>>         SourceExpression     { get; set; }
    public BindingValueChangedHandler          TargetChangedHandler { get; set; }

    public void Reset()
    {
        BindingEngine.ClearBinding(Binding);
        Apply();
    }

    public virtual void Apply()
    {
        Binding = BindingEngine.SetPropertyBinding(Target, TargetExpression, Source, SourceExpression);
        if(TargetChangedHandler != null)
            Binding.SetTargetChanged(TargetChangedHandler);
    }

    public virtual CommandVmPropertyBinding Clone(CommandVm target)
    {
        var ret = new CommandVmPropertyBinding()
        {
            Target               = target,
            TargetExpression     = TargetExpression,
            SourceExpression     = SourceExpression,
            TargetChangedHandler = TargetChangedHandler
        };

        if (Source == Target)
            ret.Source = target;

        return ret;
    }

    protected WeakPropertyBinding Binding;
}

public class CommandVmPropertyBinding<T>:CommandVmPropertyBinding 
{
    public new T                                   Source               { get=>(T)base.Source; set=>base.Source = value; }
    public new Expression<Func<T, object>>         SourceExpression     { get; set; }
    
    public override void Apply()
    {
        Binding = BindingEngine.SetPropertyBinding(Target, TargetExpression, Source, SourceExpression);
        if(TargetChangedHandler != null)
            Binding.SetTargetChanged(TargetChangedHandler);
    }

    public override CommandVmPropertyBinding<T> Clone(CommandVm target)
    {
        var ret = new CommandVmPropertyBinding<T>()
        {
            Target               = target,
            TargetExpression     = TargetExpression,
            SourceExpression     = SourceExpression,
            TargetChangedHandler = TargetChangedHandler
        };

        if (object.ReferenceEquals(Source, Target))
            ret.Source = (T)Convert.ChangeType(target, typeof(T));

        return ret;
    }

}


