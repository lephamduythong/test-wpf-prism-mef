using DemoTest1.Core.Events;
using DemoTest1.Infrastructure;
using ModuleA.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoTest1
{
    [Export]
    public class ShellViewModel : BindableBase /*, IDataErrorInfo*/
    {
        private string _title = "Hello World";

        public string Title { get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }

        IEventAggregator _eventAggregator;
        IRegionManager _regionManager;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(TitleInputChanged);

            NavigateCommand = new DelegateCommand<string>(Navigate);

            NotificationRequest = new InteractionRequest<INotification>();
            NotificationCommand = new DelegateCommand(RaiseNotification);
        }

        private void TitleInputChanged(string message)
        {
            Title = message;
        }

        // Validation

        //public string ValidateInputText
        //{
        //    get;
        //    set;
        //}

        //public ICommand ValidateInputCommand
        //{
        //    get { return new RelayCommand(); }
        //    set { }
        //}

        //private int age = 20;

        //public int Age
        //{
        //    get { return age; }
        //    set { age = value; }
        //}
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        if ("ValidateInputText" == columnName)
        //        {
        //            if (String.IsNullOrEmpty(ValidateInputText))
        //            {
        //                return "Please enter a Name";
        //            }
        //        }
        //        else if ("Age" == columnName)
        //        {
        //            if (Age < 0)
        //            {
        //                return "age should be greater than 0";
        //            }
        //        }
        //        return "";
        //    }
        //}
        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public DelegateCommand<string> NavigateCommand { get; set; }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                switch (navigatePath)
                {
                    case "ContentView":
                        _regionManager.RequestNavigate(Constants.RegionNames.ContentRegion, new Uri(nameof(ContentView), UriKind.Relative));
                        break;
                    case "NavigationContentView":
                        _regionManager.RequestNavigate(Constants.RegionNames.ContentRegion, new Uri(nameof(NavigationContentView), UriKind.Relative));
                        break;
                }
            }
        }

        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public DelegateCommand NotificationCommand { get; set; }

        void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" }, r => Title = "Notified");
        }
    }

    class RelayCommand : ICommand
    {
        public void Execute(object parameter)
        {

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}
