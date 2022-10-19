/********************************************************************
    created:	2020/4/28 19:48:20
    author:		rush
    email:		
	
    purpose:	脚本孵化器
                基本样式同CTrader
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NeoTrader.Scripts
{
    public interface IScriptIncubator:INotifyPropertyChanged
    { 
        bool                IsSystem { get; }                               // 是否是系统指标（不能修改，只能用于回测（主要用于测试））
        bool                IsIndicator { get; }                            // 是否是Indicator，否则strategy
        string              Name { get; set; }                              // Script Name, 以及Project Name

        bool                IsValid { get; }                                // 是否是正确的（如果人为在外部删除重要文件，该incubator将无效）


        string              SlnDir { get; }
        string              SlnPath { get; }
        string              ProjDir { get; }
        string              ProjPath { get; }
        string              DllPath { get; }                                // 对应Dll的Path
        DateTime?           LastCompiledTime { get; }              // dll 对应的codefile的修改时间，如果CodeFile有新的修改，则需要重编Dll

        string              Code { get; }                                   // 代码
        string              CodePath { get; }                               // 代码存储路径件
//        bool                IsCodeFileExist { get; }

//        bool                Save();                                         // 存储代码到文件

//        IFactory            ScriptFactory { get; }                          // 对应的ScriptFactory

        bool                Compile();                                      // 编译
        bool                IsNeedCompile { get; }

        ObservableCollection<IncubatorInstance> Instances { get; } // 实例

        bool                ShowInExplore();
        bool                EditInVs();                                   // 
    }


public enum ETimeFrame
{
    Unknown = 0,
    T1      = 1, // 一个tick

    M1  = 2,  // 1分钟bar
    M3  = 4,  // 3分钟bar
    M5  = 8,  // 5分钟bar
    M15 = 16, // 15分钟bar
    M30 = 32, // 15分钟bar

    H1 = 64, // 1小时

    H2			= 128,			// 2小时
    H4 = 256, // 4小时

    D1 = 1024,

    W1 = 2048, // 1周

    MN1 = 4096, // 1月

    Y1 = 8192, // 年

    Minutes = 62,    // 所有分钟
    Hours   = 448,   // 所有小时
    All     = 15871, // 所有tf
}

public class ChartDM // Chart的数据
{
    public SymbolContract SSDM; // symbol sdm 
    public ETimeFrame     TF = ETimeFrame.D1;
}


public struct SymbolContract
{
    public string Name { get; set; }

#region 通常该4个参数可定义connection下的唯一symbol，兼容IB

    public string      Code       { get; set; }
#endregion

    public string ProviderName { get; set; }
    public Guid?  ProviderId   { get; set; }

      public SymbolContract(string code, string name,string providerName, Guid? providerId = null)
    {
        Code       = code.ToUpper();
        Name       = name;
     
        ProviderName = providerName;
        ProviderId   = providerId;
    }

  }

public class IncubatorInstanceDM
{
    public ChartDM ChartDM { get; set; }
    //public BackTestingDM BackTestingDM { get; set; }
    //public OptimizationDM OptimizationDM { get; set; }
}

  public class IncubatorInstance:ObservableObject
    {
        public IncubatorInstanceDM ToDM()
        {
            return new IncubatorInstanceDM()
            {
                ChartDM =  ChartDM,
                //ChartDM = _Chart?.ToDm() ?? ChartDM,
                //BackTestingDM = (BackTesting as BackTesting).ToDM(),
                //OptimizationDM = (Optimization as Optimization).ToDM()
            };
        }


        public IScriptIncubator Host { get; set; }
        public ChartDM      ChartDM { get; set; }

        //public IScript      Script
        //{
        //    get { return _Script; }
        //    set
        //    {
        //        if (value == _Script)
        //            return;

        //        _Script = value;

        //        RaisePropertyChanged("Script");
        //    }
        //}

        //public IChart       Chart
        //{
        //    get { return _Chart;}
        //    set
        //    {
        //        if (value == _Chart)
        //            return;

        //        IChart old = _Chart;
        //        _Chart = value as Chart;

        //        if (value != null)
        //        {
        //            value.Scripts.CollectionChanged += (s, e) =>
        //            {
        //                if (e.Action == NotifyCollectionChangedAction.Add)
        //                {
        //                    if ((e.NewItems[0] as Script).IsPreset)  // 预置项就是instance script
        //                        Script = e.NewItems[0] as IScript;
        //                }
        //            };

        //            IScript sc = value.Scripts.FirstOrDefault(p => (p as Script).IsPreset);
        //            if (sc != null)
        //                Script = sc;
        //        }

        //        RaisePropertyChanged("Chart", value, old);
        //    } }                             // chart 如果有
        //public IBackTesting BackTesting { get; set; }
        //public IOptimization Optimization { get; set; }

        public IncubatorInstance(IScriptIncubator host, IncubatorInstanceDM dm)
        {
            Host = host;
            ChartDM = dm.ChartDM;
            //BackTesting = RReflector.CreateInstance<BackTesting>(this, ChartDM.SSDM, ChartDM.TF, dm.BackTestingDM);
            //Optimization = RReflector.CreateInstance<Optimization>(this, ChartDM.SSDM, ChartDM.TF, dm.OptimizationDM);
        }

        //protected Chart    _Chart;
    }
}
