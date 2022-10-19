/********************************************************************
    created:	2020/4/28 20:16:58
    author:		rush
    email:		
	
    purpose:	
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Charts.Native;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpo.Logger;
using RLib.Base;

namespace NeoTrader.Scripts
{
public class ScriptIncubatorDm
{
   public string Name { get; set; }

    public List<IncubatorInstanceDM> Instances { get; set; }
}

public class ScriptIncubator : ObservableObject, IScriptIncubator, ICloneable{
    [Browsable(false)]
    public Guid Id { get; set; }

    [Browsable(false)]
    public Guid UserId { get; set; }

    public double Index { get => Index_; set => SetProperty(ref Index_, value); }

    public override string ToString() => $"{Name}";

     public ScriptIncubator(string name, bool isIndicator = true, bool isSystem = true)
    {
        Id     = Guid.NewGuid();

        IsSystem    = isSystem;
        IsIndicator = isIndicator;
        Name        = name;

    }

     public ScriptIncubatorDm ToDm()
     {
         return new ScriptIncubatorDm()
         {
             Name      = Name,
             Instances = Instances.Select(p => p.ToDM()).ToList()
         };
     }

     public ScriptIncubator(ScriptIncubatorDm dm)
     {
         Name = dm.Name;

         dm.Instances.ForEach(p=>Instances.Add(new IncubatorInstance(this, p)));
     }

    public virtual object Clone() => this.MemberwiseClone();

    protected string SrcDir
    {
        get { return "."; }
    }

    public bool IsSystem    { get; set; } // 是否是系统指标（不能修改，只能用于回测（主要用于测试））
    public bool IsIndicator { get; set; } // 是否是Indicator

    public string Name
    {
        get { return _Name; }
        set
        {
            if (value == Name)
                return;
            // 修改
            _Name = value;
            OnPropertyChanged();
        }
    } // 即script Name

    public bool IsValid { get { return _IsValid; } set { SetProperty(ref _IsValid, value); } } // 是否是正确的（如果人为在外部删除重要文件，该incubator将无效）

    public bool CheckValid()
    {
        IsValid = true;
       return true;
    }


    public string SlnDir   => PathEx.CombineRelative(SrcDir, Name);
    public string SlnPath  => PathEx.CombineRelative(SlnDir, Name + ".sln");
    public string ProjDir  => PathEx.CombineRelative(SlnDir, Name);
    public string ProjPath => PathEx.CombineRelative(ProjDir, Name + ".csproj");
    public string CodePath => PathEx.CombineRelative(ProjDir, $"{Name}.cs"); // 代码存储路径

    public string TempDllPath => PathEx.CombineRelative(ProjDir, $"{Name}.cs"); // 代码存储路径

    //public bool         IsCodeFileExist
    //{
    //    get { return _IsCodeFileExist; }
    //}

    protected bool IsCodeFileExist() { return File.Exists(CodePath); }

    public string DllPath => PathEx.CombineRelative(SrcDir, Name) + ".ev"; // 对应Dll的Path

    public DateTime? LastCompiledTime
    {
        get { return _DllCorrespondCodeFileTime; }
        set
        {
            IsNeedCompile = false;
            SetProperty(ref _DllCorrespondCodeFileTime, value);
        }
    } // dll 对应的codefile的修改时间，如果CodeFile有新的修改，则需要重编Dll

    public string Code { get; } // 代码

    public bool Compile() // 编译
    {

        return true;
    }

    public bool IsNeedCompile { get { return _IsNeedCompile; } set { SetProperty(ref _IsNeedCompile, value); } }

    protected void CheckNeedCompile_() // 检查是否需要编译
    {
        if (IsSystem)
        {
            IsNeedCompile = false;
            return;
        }

        // 如果不存在sln，不需要comp
        if (!File.Exists(SlnPath))
        {
            IsNeedCompile = false;

            return;
        }

        // dll 不存在， 必然需要重编
        if (!File.Exists(DllPath))
        {
            IsNeedCompile = true;

            return;
        }


        // 两者都存在的情况下，比较上一次dll编译所使用的File版本
        if (LastCompiledTime != null)
        {
            FileInfo fi = new FileInfo(CodePath);
            IsNeedCompile = (LastCompiledTime != fi.LastWriteTime);
        }
    }

  

    public ObservableCollection<IncubatorInstance> Instances { get { return Instances_; } } // 实例


    public bool ShowInExplore()
    {
        string str = SlnPath;

        //if (File.Exists(DllPath))
        //    str = DllPath;

        System.Diagnostics.Process.Start("Explorer.exe", "/select," + str);

        return true;
    }

    public bool EditInVs() // 
    {
        if (File.Exists(SlnPath))
        {
            //System.Diagnostics.Process.Start(ProjPath);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //string sln = @"D:\1.xls";

                Process.Start(new ProcessStartInfo(@"cmd", $"/c start {SlnPath}")
                {
                    UseShellExecute = false,
                });
            }

            //var proc = System.Diagnostics.Process.Start(@"cmd.exe ", $"/c {SlnPath}");
            return true;
        }
        else
            return false;
    }

    public bool Init()
    {
               return true;
    }


    protected string                                  _Name;
    protected double                                  Index_;
    public    ObservableCollection<IncubatorInstance> Instances_ = new();

    protected DateTime? _DllCorrespondCodeFileTime;

    protected bool _IsNeedCompile;

    protected bool _IsCodeFileExist;

    protected bool _IsValid;
}
}