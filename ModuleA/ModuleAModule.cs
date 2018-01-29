using Microsoft.Practices.ServiceLocation;
using ModuleA.Views;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTest1.Infrastructure;
using ModuleB.Views;

namespace ModuleA
{
    [ModuleExport(typeof(ModuleAModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class ModuleAModule : IModule
    {
        // [Import]
        private IRegionManager _regionManager;
        // [Import]
        private IServiceLocator _serviceLocator;

        [ImportingConstructor]
        ModuleAModule(IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Constants.RegionNames.ToolbarRegion, typeof(ToolbarView));
            _regionManager.RegisterViewWithRegion(Constants.RegionNames.ContentRegion, typeof(ContentView));
            _regionManager.RegisterViewWithRegion(Constants.RegionNames.ContentRegion, typeof(NavigationContentView));

            //var contentViewRegion = _regionManager.Regions[Constants.RegionNames.ContentRegion];
            //var contentView = _serviceLocator.GetInstance(typeof(ContentView));
            //// contentViewRegion.Add(contentView);   
            //contentViewRegion.Deactivate(contentView);
        }
    }
}
