using DemoTest1.Core;
using DemoTest1.Core.Events;
using DemoTest1.Infrastructure;
using ModuleA.Models;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    [Export]
    public class ContentViewModel : BindableBase // , IActiveAware
    {
        [ImportingConstructor]
        public ContentViewModel(IEventAggregator eventAggregator)
        {
            ExecuteDelegateCommand = new DelegateCommand(Execute, CanExecute);

            DelegateCommandObservesProperty = new DelegateCommand(Execute, CanExecute)
                .ObservesProperty(() => IsEnabled);

            DelegateCommandObservesCanExecute = new DelegateCommand(Execute)
                .ObservesCanExecute(() => IsEnabled);

            ExecuteGenericDelegateCommand = new DelegateCommand<string>(ExecuteGeneric)
                .ObservesCanExecute(() => IsEnabled);

            //_applicationCommands = applicationCommands;

            //ApplicationGlobalSaveCommand = new DelegateCommand(ApplicationGlobalSave);
            //_applicationCommands.SaveCommand.RegisterCommand(ApplicationGlobalSaveCommand);

            ApplicationGlobalSaveCommand = new DelegateCommand(ApplicationGlobalSave);
            GlobalCommands.SaveAllCommand.RegisterCommand(ApplicationGlobalSaveCommand);

            _eventAggregator = eventAggregator;
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        // -----------------------

        //private IApplicationCommands _applicationCommands;
        //public IApplicationCommands ApplicationCommands
        //{
        //    get { return _applicationCommands; }
        //    set { SetProperty(ref _applicationCommands, value); }
        //}

        // -----------------------

        public DelegateCommand ExecuteDelegateCommand { get; private set; }
        public DelegateCommand DelegateCommandObservesProperty { get; private set; }
        public DelegateCommand DelegateCommandObservesCanExecute { get; private set; }
        public DelegateCommand<string> ExecuteGenericDelegateCommand { get; private set; }
        public DelegateCommand ApplicationGlobalSaveCommand { get; set; }

        private void ApplicationGlobalSave()
        {
            MessageBox.Show("Global Save");
        }

        private void Execute()
        {
            Content = $"Updated: {DateTime.Now}";
        }

        private bool CanExecute()
        {
            return !IsEnabled;
        }

        private void ExecuteGeneric(string parameter)
        {
            Content = parameter;
        }

        private bool _isEnabled;
        public bool IsEnabled { get => _isEnabled;
            set {
                SetProperty(ref _isEnabled, value);
                ExecuteDelegateCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand TestCommand => new DelegateCommand(this.Test);
        // .ObservesProperty(() => Content);

        // IApplicationCurrentInstance

        private string _content;

        public string Content {
            get
            {
                return this._content;
            }
            set
            {
                SetProperty(ref _content, value);
                // SendTitleMessage();
            }
        }

        private void Test()
        {
            MessageBox.Show(this._content);
        }

        //private void SendTitleMessage()
        //{
        //    _eventAggregator.GetEvent<MessageSentEvent>().Publish(Content);
        //}

        // public event EventHandler IsActiveChanged;

        // public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IEventAggregator _eventAggregator;

        public DelegateCommand SendMessageCommand { get; set; }

        private void SendMessage()
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(Content);
        }

        
    }
}
