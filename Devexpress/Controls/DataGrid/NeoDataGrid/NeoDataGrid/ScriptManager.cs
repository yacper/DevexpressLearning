/********************************************************************
    created:	2017/3/28 11:08:11
    author:		rush
    email:		
	
    purpose:	

*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RLib.Base;

namespace NeoTrader.Scripts
{
public class ScriptManager {
    public string ApiDir             => "Api";     // 源目录
    public string ApiDllName         => "Neo.Api.dll";                                             // 源目录
    public string ApiDllPath         => ApiDir.CombinePath(ApiDllName);                            // 源目录
    public string SourceDir          => "Sources"; // 源目录
    public string SourceIndicatorDir => SourceDir.CombinePath("Indicators");
    public string SourceStrategyDir  => SourceDir.CombinePath("Strategies");
    public string TempDllDir         => "TempDll";


    public ObservableCollection<IScriptIncubator> IndicatorIncubators => IndicatorIncubators_;
    public ObservableCollection<IScriptIncubator> StrategyIncubators  => StrategyIncubators_;
    public IEnumerable<IScriptIncubator>          Incubators          => IndicatorIncubators.Union(StrategyIncubators);


    public IScriptIncubator CreateScriptIncubator(string name = null, bool indicator = true) // 创建
    {
        string templateDir = "Resources\\Scripts\\Templates\\";

        string resName = "NewIndicator";
        if (!indicator)
            resName = "NewStrategy";
        string resPath = templateDir + resName;

        string tempDir     = "temp\\";
        string tempResPath = tempDir + resPath;

        if (name != null)
        {
            if (Incubators.Any(p => p.Name == name))
                return null;
        }
        else
        {
            name = resName + "_";
        }


          // 2.编译源码dll放到文件夹 
        ScriptIncubator si = new ScriptIncubator(name) { IsIndicator = indicator };
        si.Init();

        if (indicator)
            IndicatorIncubators.Add(si);
        else
            StrategyIncubators.Add(si);

        return si;
    }

    public void DeleteScriptIncubator(IScriptIncubator si) // 
    {

        if (si.IsIndicator)
            IndicatorIncubators.Remove(si);
        else
            StrategyIncubators.Remove(si);
    }


    public IScriptIncubator CopyScriptIncubator(IScriptIncubator sb) // 
    {
        //todo:目录下递增
        var name = sb.Name;
        name += "+";

        return CreateScriptIncubator(name, sb.IsIndicator);
    }


  

#region C&D

    protected async void LoadIncubators_()
    {
            //File.ReadAllText("Indicator.json").ToJson<List<ScriptIncubatorDm>()

        //foreach (var si in entities)
        //{
        //    ScriptIncubator inc = new ScriptIncubator(si);
        //    if (!inc.CheckValid()) // 已经被用户删除了
        //        continue;

        //    inc.Init();

        //    if (inc.IsIndicator)
        //        IndicatorIncubators_.Add(inc);
        //    else
        //        StrategyIncubators_.Add(inc);
        //}


        //IndicatorIncubators_.CollectionChanged += Incubators__CollectionChanged;
        //StrategyIncubators_.CollectionChanged  += Incubators__CollectionChanged;

    }

    //protected void Save()
    //{
    //    File.WriteAllText("Indicator.json", IndicatorIncubators.OfType<ScriptIncubator>().Select(p => p.ToDm()).ToList().ToJson());
    //    File.WriteAllText("Strategy.json", StrategyIncubators.OfType<ScriptIncubator>().Select(p => p.ToDm()).ToList().ToJson());
    //}



    public ScriptManager()
    {
    
    }

    private async void Incubators__CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
            {
                ScriptIncubator inc    = e.NewItems[0] as ScriptIncubator;
                //var             entity = await UserService.Add(inc.ToEntity());
                //inc.Index = entity.Index;

                inc.PropertyChanged += Inc_PropertyChanged;
            }
                break;
            case NotifyCollectionChangedAction.Remove:
            {
                ScriptIncubator inc = e.OldItems[0] as ScriptIncubator;
                //await UserService.Remove(inc.ToEntity());

                inc.PropertyChanged -= Inc_PropertyChanged;
            }
                break;
             case NotifyCollectionChangedAction.Move:
            {
                        //todo: 调换位置

                ScriptIncubator inc = e.OldItems[0] as ScriptIncubator;
                //await UserService.Remove(inc.ToEntity());

                inc.PropertyChanged -= Inc_PropertyChanged;
            }
                break;
            
        }
    }

    private async void Inc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        //var entity = await UserService.Update((sender as ScriptIncubator).ToEntity());
        //(sender as ScriptIncubator).Index = entity.Index;
    }

#endregion


#region Members

    protected ObservableCollection<IScriptIncubator> IndicatorIncubators_ = new();
    protected ObservableCollection<IScriptIncubator> StrategyIncubators_  = new();

#endregion
}
}