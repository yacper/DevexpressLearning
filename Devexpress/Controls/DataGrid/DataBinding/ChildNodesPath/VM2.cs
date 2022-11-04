// created: 2022/11/03 17:34
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChildNodesPath
{
public enum ETimeFrame
{
    T1,
    M1,
    M3,
    M5,
    H1,
    D1,
    W1
}

public class IncubatorInstance
{
    public override string ToString() => $"Instance {Name},{TimeFrame}";

    public          string     Name      => Symbol;
    public          string     Symbol    { get; set; }
    public          ETimeFrame TimeFrame { get; set; }
}

public class ScriptIncubator
{
    public override string ToString() => $"Incubator {Name}";
    public string Name { get; set; }

    public void Compile() { Debug.WriteLine($"{Name} Compile..."); }

    public ObservableCollection<IncubatorInstance> Instances { get; set; }
}


public class VM2
{
    public ObservableCollection<ScriptIncubator> Incubators { get; set; }

    public VM2()
    {
        Incubators = new()
        {
            new()
            {
                Name = "Mva",
                Instances = new()
                {
                    new IncubatorInstance() { Symbol = "XAUUSD", TimeFrame = ETimeFrame.M5 },
                }
            },
            new()
            {
                Name = "Rsi",
                Instances = new()
                {
                    new IncubatorInstance() { Symbol = "GBP", TimeFrame = ETimeFrame.M1 },
                    new IncubatorInstance() { Symbol = "EUR", TimeFrame = ETimeFrame.D1 },
                }
            },
            new()
            {
                Name = "Atr",
                Instances = new()
                {
                    new IncubatorInstance() { Symbol = "Jbp", TimeFrame = ETimeFrame.D1 },
                }
            }
        };

        foreach (var inc in Incubators)
        {
            inc.Instances.CollectionChanged += (s, e) =>
            {
                string changedInfo = "";
                changedInfo += $"Instance {s.ToString()} Action:  {e.Action} ---";
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        foreach (var item in e.NewItems)
                            changedInfo += $"newIdx: {e.NewStartingIndex}, Data: {item} \n";
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        foreach (var item in e.OldItems)
                            changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {item} \n";
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                        break;
                    default:
                        break;
                }

                Debug.WriteLine(changedInfo);
            };
        }

        Incubators.CollectionChanged += (s, e) =>
        {
            string changedInfo = "";
            changedInfo += $"Incubator {s.ToString()} Action:  {e.Action} ---";
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                        changedInfo += $"newIdx: {e.NewStartingIndex}, Data: {item} \n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                        changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {item} \n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    changedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                default:
                    break;
            }

            Debug.WriteLine(changedInfo);
        };
    }
}
}