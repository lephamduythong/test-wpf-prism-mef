using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest1.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand SaveCommand { get; }
    }
    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _saveCommand = new CompositeCommand();
        public CompositeCommand SaveCommand
        {
            get { return _saveCommand; }
        }
    }

    public static class GlobalCommands
    {
        public static CompositeCommand SaveAllCommand = new CompositeCommand();
    }
}
