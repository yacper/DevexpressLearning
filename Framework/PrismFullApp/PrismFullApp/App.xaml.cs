using Prism.Ioc;
using Prism.Modularity;
using PrismFullApp.Modules.ModuleName;
using PrismFullApp.Services;
using PrismFullApp.Services.Interfaces;
using PrismFullApp.Views;
using System.Windows;

namespace PrismFullApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
