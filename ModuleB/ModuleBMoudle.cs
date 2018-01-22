using DemoTest1.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using ModuleB.Views;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB
{
    [ModuleExport(typeof(ModuleBMoudle), InitializationMode = InitializationMode.WhenAvailable)]
    public class ModuleBMoudle : IModule
    {
        // [Import]
        private IRegionManager _regionManager;
        // [Import]
        private IServiceLocator _serviceLocator;

        [ImportingConstructor]
        ModuleBMoudle(IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Constants.RegionNames.ExtraRegion, typeof(MessageListView));
        }
    }
}
