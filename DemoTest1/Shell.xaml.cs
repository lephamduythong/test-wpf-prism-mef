using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoTest1
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        private IModuleManager _moduleManager;
        private ShellViewModel _shellViewModel;

        [ImportingConstructor]
        public Shell(IModuleManager moduleManager)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
        }

        [Import]
        public ShellViewModel ViewModel
        {
            get { return _shellViewModel; }
            set
            {
                DataContext = value;
                _shellViewModel = (ShellViewModel)DataContext;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // _moduleManager.LoadModule("ModuleAModule");
        }
    }
}
