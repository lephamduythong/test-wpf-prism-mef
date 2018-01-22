using DemoTest1.Core.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest1
{
    [Export]
    public class ShellViewModel : BindableBase
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
    }
}
