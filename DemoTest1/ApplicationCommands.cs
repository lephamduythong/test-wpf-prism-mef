using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest1.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand SaveCommand { get; }
    }

    [Export]
    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _saveCommand = new CompositeCommand();

        public CompositeCommand SaveCommand
        {
            get { return _saveCommand; }
        }
    }
}
