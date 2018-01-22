using DemoTest1.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using ModuleA.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    [Export]
    public class ToolbarViewModel : BindableBase
    {
        [Import]
        private IRegionManager regionManager;
        [Import]
        private IServiceLocator serviceLocator;

        public ICommand ActiveContentViewCommand => new DelegateCommand(this.ActiveContentView);
        public ICommand DeactivateContentViewCommand => new DelegateCommand(this.DeactivateContentView);

        private void ActiveContentView()
        {
            try
            {
                var region = regionManager.Regions[Constants.RegionNames.ContentRegion];
                // var view = region.GetView("ContentView");
                var view = serviceLocator.GetInstance(typeof(ContentView));
                // region.Add(view);
                region.Activate(view);
            }
            catch (Exception)
            {

            }
        }

        private void DeactivateContentView()
        {
            try
            {
                var region = regionManager.Regions[Constants.RegionNames.ContentRegion];
                var view = serviceLocator.GetInstance(typeof(ContentView));
                // var view = region.GetView("ContentView");
                region.Deactivate(view);
            }
            catch (Exception)
            {

            }
        }
    }
}
