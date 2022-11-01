using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using NeoTrader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeoControls
{
    //   1.LinkButton  2.ToogleButton

    public enum ETimeFrame
    {
        tick = 1,

        m1 = 2,
        m5 = 8,
        m15 = 16,
        m30 = 32,
        m60 = 64,
        m120 = 128,
        D1 = 1024,
        W1 = 2048,
        M1 = 4096
    }

    public enum ELinkStatus
    {
        None,       
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
    }

    public class Symbol: BindableBase
    {
        public virtual ETimeFrame TF { get => GetProperty(() => TF); set => SetProperty(() => TF, value); }
        public virtual ELinkStatus ELinkStatus { get => GetProperty(() => ELinkStatus); set => SetProperty(() => ELinkStatus, value); }

        public Symbol()
        {
            TF = ETimeFrame.D1;
            ELinkStatus = ELinkStatus.None;
        }
    }

    /// <summary>
    /// WorkSpaceTools.xaml 的交互逻辑
    /// </summary>
    public partial class WorkSpaceTools : UserControl
    {
        public Symbol Symbol { get; set; }
        public ObservableCollection<CommandVm> LeftVms { get; set; }
        public ObservableCollection<CommandVm> RightVms { get; set; }
        public WorkSpaceTools()
        {
            InitializeComponent();
            this.DataContext = this;
            InitData();
        }

        private void InitData()
        {
            Symbol = new Symbol();

            LeftVms = new ObservableCollection<CommandVm>()
            {
                new CommandVm()
                {
                    DisplayName = $"{Symbol.TF}",
                    DisplayMode = BarItemDisplayMode.Content,
                    Commands = new ObservableCollection<CommandVm>()
                    {
                        new CommandVm()
                        {
                            DisplayName = "1D",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.D1;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "Tick",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.tick;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "M1",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.M1;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "m120",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.m120;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "m60",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.m60;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "m15",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.m15;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "m6",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.m5;
                            })
                        },
                        new CommandVm()
                        {
                            DisplayName = "W1",
                            Command = new DelegateCommand(() =>
                            {
                                Symbol.TF = ETimeFrame.W1;
                            })
                        },
                    }
                }.WithPropertyBinding<Symbol>(o=>o.DisplayName, Symbol, S=>S.TF),

                //new CommandVm()
                //{
                //    Glyph = Images.Link,
                //    IsLink = true,
                //    Commands = new ObservableCollection<CommandVm>()
                //    {
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            Glyph = Images.Link,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.None;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.One;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Two;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Three;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Four;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Five;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Six;
                //            })
                //        },
                //        new CommandVm()
                //        {
                //            DisplayMode = BarItemDisplayMode.ContentAndGlyph,
                //            IsLink= true,
                //            Command = new DelegateCommand(() =>
                //            {
                //                Symbol.ELinkStatus = ELinkStatus.Seven;
                //            })
                //        },
                //    }
                //}
            };

            RightVms = new ObservableCollection<CommandVm>() 
            {
                
            };
        }

        

    }

}
