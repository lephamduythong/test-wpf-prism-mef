using DemoTest1.Core.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.ViewModels
{
    [Export]
    public class MessageListViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages { get => _messages;
            set
            {
                SetProperty(ref _messages, value);
            }
        }

        [ImportingConstructor]
        public MessageListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Messages = new ObservableCollection<string>();
            Messages.Add("First blood");

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageRecieved);

            // Filter
            //_eventAggregator.GetEvent<MessageSentEvent>().Subscribe(
            //    MessageRecieved,
            //    ThreadOption.PublisherThread,
            //    false,
            //    (filter) => filter.Contains("ThongAss")
            //);
        }
        
        private void MessageRecieved(string message)
        {
            Messages.Add(message);
        }
    }
}
