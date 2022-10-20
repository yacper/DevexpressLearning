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

public enum DisplayMode
{
    Content = 0,
    Glyph,
    ContentAndGlyph
}

public class RowToolsViewMode: CommandViewModel
{
    public bool IsNormalUse { get; set; }                                   // 是否通用，通用显示在RowControl上, 不通用现在 More 下面    
    public bool IsGlyphText { get; set; }                                   // 只是 文本和图片
    public new DisplayMode DisplayMode                                      // 显示模式，扩展了父类，添加了单独的 Glyph 显示
    {
        get => _DisplayMode;
        set => SetValue(ref _DisplayMode, value);
    }
    public override string DisplayName                                      // 显示文本
    {
        get => _DisplayName;
        set => SetValue(ref _DisplayName, value);
    }
    public double DisplayTextWidth { get; set; }                              // 显示文本宽度
    public Dock GlyphDock { get; set; }                                       // 图片 Dock 对齐方式    
    public TextAlignment DisplayTextAlign { get; set; }                       // 显示文本对齐方式
    public override ImageSource Glyph                                         // 显示的图片 
    {
        get => _Glyph;
        set => SetValue(ref _Glyph, value);
    }
    public bool TipGlyphShow                                                  // 小标记是否显示
    {
        get => _TipGlyphShow;
        set => SetValue(ref _TipGlyphShow, value);
    }
    public ImageSource TipGlyph { get; set; }                                 //  小标记，类似未读邮件的小红点标志
    public bool IsExpand { get; set; }                                        // 按钮是否是下拉功能
    public object ToolTip                                                     // ToolTip 
    { 
        get => _ToolTip;
        set=>SetValue(ref _ToolTip, value); 
    }
    public Brush BackgroundBrush { get; set; }                                // 背景色
    public Brush BorderBrush { get; set; }                                    // 边框色
    public Thickness BorderThickness { get; set; }                            // 边框大小    

    public RowToolsViewMode()
    {
        ToolTip = "";
        
        BorderBrush = Brushes.Transparent;
        BorderThickness = new Thickness(0);
        BackgroundBrush = Brushes.Transparent;
    }

    private DisplayMode _DisplayMode;
    private string _DisplayName = "";
    private ImageSource _Glyph;
    private Object _ToolTip;
    private bool _TipGlyphShow;
}

public interface IRowTools
{
    public ObservableCollection<RowToolsViewMode> ToolVMs { get; }
    public RowToolsViewMode SelectedToolVm { get; }
    public bool ToolIsFixed { get; }
    public Brush ToolsBgBrush { get; }
    public int Level { get;  }
}

public class RowTools : IRowTools
{
    public ObservableCollection<RowToolsViewMode> ToolVMs { get; protected set; }   //  tools 实体
    public RowToolsViewMode SelectedToolVm { get; set; }                            // 当前选中的控件
    public bool ToolIsFixed { get; set; }                                           // tools  是否固定在 row上
    public Brush ToolsBgBrush { get; set; } 
    public int Level { get; set; }                                                  // 作用的 RowControl Level
    public RowTools(bool toolIsFixed = false)
    {
        Level = -1;
        ToolVMs = new();
        ToolIsFixed = toolIsFixed;
        ToolsBgBrush = Brushes.Transparent;
    }

    public RowTools(int level, bool toolIsFixed = false): this(toolIsFixed)
    {
        Level = level;
    }

    public void AddVM(RowToolsViewMode vm)
    {
        if (ToolVMs == null)
            ToolVMs = new();
        ToolVMs.Add(vm);
    }

    public void InsertVM(int idx, RowToolsViewMode vm)
    {
        if (ToolVMs == null)
            ToolVMs = new();

        if (idx < 0 || idx >= ToolVMs.Count)
            throw new ArgumentOutOfRangeException($"{nameof(idx)}: {idx} 不合法");

        ToolVMs.Insert(idx, vm);
    }

    public bool RemoveVM(RowToolsViewMode vm)
    {
        if (ToolVMs == null)
            throw new Exception("ToolVMs 没有初始化");
        return ToolVMs.Remove(vm);
    }

    public void RemoveVMAt(int idx)
    {
        if (idx < 0 || idx >= ToolVMs.Count)
            throw new ArgumentOutOfRangeException("idx 参数合法");

        if (ToolVMs == null)
            throw new Exception("ToolVMs 没有初始化");

        ToolVMs.RemoveAt(idx);
    }

    public void AppendVms(IEnumerable<RowToolsViewMode> vms)
    {
        if (vms == null || vms.Count() == 0)
            return;

        if (ToolVMs == null)
            throw new Exception("ToolVMs 没有初始化");

        foreach (var vm in vms)
        {
            ToolVMs.Add(vm);
        }
    }
}