using Meerkat.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Meerkat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            this.container = new StandardKernel();

            base.OnStartup(e);
        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
            container.Bind<VimStateMachine>().To<VimStateMachine>();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Meerkat";
        }
    }
}
