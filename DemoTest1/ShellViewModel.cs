using DemoTest1.Core.Events;
using Prism.Events;
using Prism.Mvvm;
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
    public class ShellViewModel : BindableBase, IDataErrorInfo
    {
        private string _title = "XXXX";

        public string Title { get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }

        IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(TitleInputChanged);
        }

        private void TitleInputChanged(string message)
        {
            Title = message;
        }

        // Validation

        public string ValidateInputText
        {
            get;
            set;
        }

        public ICommand ValidateInputCommand
        {
            get { return new RelayCommand(); }
            set { }
        }

        private int age = 20;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string this[string columnName]
        {
            get
            {
                if ("ValidateInputText" == columnName)
                {
                    if (String.IsNullOrEmpty(ValidateInputText))
                    {
                        return "Please enter a Name";
                    }
                }
                else if ("Age" == columnName)
                {
                    if (Age < 0)
                    {
                        return "age should be greater than 0";
                    }
                }
                return "";
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
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
