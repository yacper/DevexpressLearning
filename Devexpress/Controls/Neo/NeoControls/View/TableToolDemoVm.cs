using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using Neo.Api.Attributes;
using NeoTrader;
using NeoTrader.UI.Controls;

namespace NeoControls.View
{
    public class Person : BindableBase
    {
        [StatAttribute]
        public string Name { get => GetProperty(() => Name); set => SetProperty(() => Name, value); }
        [StatAttribute]
        public int Age { get => GetProperty(() => Age); set => SetProperty(() => Age, value); }

        public Person()
        {
            // this.PropertyChanged += Persion_PropertyChanged;
        }

        public void AddAge()
        {
            Age++;
        }

        public void ReduceAge()
        {
            Age--;
        }

        public override string ToString()
        {
            return $" Name: {Name} - Age: {Age}";
        }

    }
    public class TableToolDemoVm : BindableBase
    {
        public int SelectedIdx { get; set; }
        public ObservableCollection<Person> People { get=>GetProperty(()=>People); set=>SetProperty(()=>People, value); }
        public CommandVm DefaultTools { get; set; }
        public ObservableCollection<CommandVm> OpVms { get; set; }
        public string CollectionChangedInfo { get => GetProperty(() => CollectionChangedInfo); set=>SetProperty(()=>CollectionChangedInfo, value); }
        public ObservableCollection<RColumnItemData> Columns { get=>GetProperty(()=>Columns); set=>SetProperty(()=>Columns, value); }

        public TableToolDemoVm()
        {
            InitData();
        }
        private void InitData()
        {
            InitPeople();
            InitDefaultTools();
            InitOpVms();
            InitColumns();
        }

        private void InitPeople()
        {
            People = new ObservableCollection<Person>();
            for (int i = 0; i < 10; i++)
            {
                People.Add(new Person() { Name = $" 张三{i}", Age = i });
            }

            People.CollectionChanged += People_CollectionChanged;
        }

        private void InitDefaultTools()
        {
            DefaultTools = new CommandVm()
            {
                Commands = new ObservableCollection<CommandVm>()
                {
                    new CommandVm()
                    {
                        DisplayName = "-",
                        Owner = People[0],
                        Command = new DelegateCommand<FrameworkContentElement>((e) =>
                        {
                            ((e.DataContext as CommandVm).Owner as Person).ReduceAge();
                        })
                    }.WithPropertyBinding(T=>T.IsEnabled, S=>(S.Owner as Person).Age, (s, e) =>
                    {
                        (e.Data.Target.Source as CommandVm).IsEnabled = ((e.Data.Target.Source as CommandVm).Owner as Person).Age > 0;
                    } ),

                    new CommandVm()
                    {
                        DisplayName = "+",
                        Owner = People[0],
                        Command = new DelegateCommand<FrameworkContentElement>((e) =>
                        {
                            ((e.DataContext as CommandVm).Owner as Person).AddAge();
                        })
                    },
                    //.WithPropertyBinding(T=>T.IsEnabled, S=>(S.Owner as Person).Age),
                    new CommandVm()
                    {
                        Glyph=Images.VMore,
                        Command= new DelegateCommand(() => { }),
                        Commands = new ObservableCollection<CommandVm>()
                        {
                            new CommandVm(){ Glyph= Images.Watchlist, DisplayName="查看List", Command = new DelegateCommand(()=>{ }) },
                            new CommandVm(){ Glyph = Images.Account, DisplayName = "用户信息", Command = new DelegateCommand(()=>{ }) },
                            new CommandVm(){ Glyph = Images.Trading, DisplayName = "交易", Command = new DelegateCommand(() => { }) },
                        }
                    }
                }
            };
        }

        private void InitOpVms()
        {
            Random random = new Random();
            OpVms = new ObservableCollection<CommandVm>()
            {
                new CommandVm()
                {
                    DisplayName = "+",
                    Command = new DelegateCommand(() =>
                    {
                        People.Add(new Person(){ Name = $"New Person {random.NextInt64(0, 100) }", Age = (int)random.NextInt64(0, 100) });
                    }),
                },                
                new CommandVm()
                {
                    DisplayName = "-",
                    Command = new DelegateCommand(() =>
                    {
                        if(People.Count > 0)
                            People.RemoveAt(0);
                    }),
                },
                new CommandVm()
                {
                    DisplayName = "Clear",
                    Command = new DelegateCommand(() =>
                    {
                        People.Clear();
                    }),
                },
                new CommandVm()
                {
                    DisplayName = "Column Op",
                    Commands = new ObservableCollection<CommandVm>()
                    {
                        new CommandVm()
                        {
                            DisplayName = "Add Age Column",
                            Command = new DelegateCommand(() =>
                            {
                                AddColumn(nameof(Person.Age), true);
                            }),
                        },
                        new CommandVm()
                        {
                            DisplayName = "Remove Age Column",
                            Command = new DelegateCommand(() =>
                            {
                                var items = Columns.Where(_=>{ return _.FieldName == nameof(Person.Age); });
                                Columns = new ObservableCollection<RColumnItemData>(Columns.Except(items));
                            }),
                        },
                        new CommandVm()
                        {
                            DisplayName = "Add Name Column",
                            Command = new DelegateCommand(() =>
                            {
                                AddColumn(nameof(Person.Name), true);
                            }),
                        },
                        new CommandVm()
                        {
                            DisplayName = "Remove Name Column",
                            Command = new DelegateCommand(() =>
                            {
                                var items = Columns.Where(_=>{ return _.FieldName == nameof(Person.Name); });
                                Columns = new ObservableCollection<RColumnItemData>(Columns.Except(items));
                            }),
                        },
                    }
                },
            };
        }

        private void InitColumns()
        {
            Columns = new ObservableCollection<RColumnItemData>();
            foreach (var p in typeof(Person).GetProperties().Where(_ => { return _.GetCustomAttribute(typeof(StatAttribute)) != null; }))
            {
                RColumnItemData rcid = new RColumnItemData();
                rcid.FieldName = p.Name;
                Columns.Add(rcid);
            }
        }

        private void AddColumn(string name, bool isFixed = false)
        {
            RColumnItemData rcid = new RColumnItemData();
            rcid.FieldName = name;
            rcid.IsFixed = isFixed;
            Columns.Add(rcid);
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CollectionChangedInfo += $"Action:  {e.Action} ---";
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:                    
                    foreach (var item in e.NewItems)
                        CollectionChangedInfo += $"newIdx: {e.NewStartingIndex}, Data: {item} \n";                    
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:                    
                    foreach (var item in e.OldItems)
                        CollectionChangedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {item} \n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    CollectionChangedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    CollectionChangedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    CollectionChangedInfo += $"oldIdx: {e.OldStartingIndex}, Data: {e.OldItems}--   newIdx: {e.NewStartingIndex}, Data: {e.NewItems}\n";
                    break;
                default:
                    break;
            }
        }
    }
}
