using DemoTest1.Core;
using ModuleA;
using ModuleB;
using Prism.Mef;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoTest1
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
            
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            //var moduleAType = typeof(ModuleAModule);
            //ModuleCatalog.AddModule(new ModuleInfo()
            //{
            //    ModuleName = moduleAType.Name,
            //    ModuleType = moduleAType.AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.OnDemand
            //});
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Container.RegisterType<IApplicationCommands, ApplicationCommands>(new ContainerControlledLifetimeManager());
        }
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleAModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleBMoudle).Assembly));

            // Module B and Module D are copied to a directory as part of a post-build step.
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Both projects have a post-build step to copy themselves into that directory.
            // DirectoryCatalog catalog = new DirectoryCatalog("DirectoryModules");
            // this.AggregateCatalog.Catalogs.Add(catalog);
        }
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;

            Application.Current.MainWindow.Loaded += MainWindowOnLoaded;

            App.Current.MainWindow.Show();
        }
        private void MainWindowOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // Add api link
        }
    }
}
