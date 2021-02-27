using Meerkat.Models;
using Meerkat.Views;
using Meerkat.ViewModels;
using Ninject;
using Stateless;
using System.Windows;
using System.Windows.Threading;
using System;

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
            base.OnStartup(e);
            container = new StandardKernel();
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
            DispatcherTimer dispatcherTimer = container.Get<DispatcherTimer>();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel();
            container.Bind<IRepository<Todo>>().To<TodoRepository>();
            container.Bind<StateMachine<State, Models.Trigger>>().ToMethod<StateMachine<State, Models.Trigger>>(context => new StateMachine<State, Models.Trigger>(State.NAVIGATION));
            container.Bind<IStateTracker, ITodoTracker>().To<MeerkatApp>().InSingletonScope();
            container.Bind<ViewModelBase>().To<MeerkatAppViewModel>();
            container.Bind<DispatcherTimer>().To<DispatcherTimer>().InSingletonScope();
        }

        private void ComposeObjects()
        {
            ViewModelBase meerkatAppViewModel = container.Get<ViewModelBase>();
            Current.MainWindow = container.Get<MainWindow>();
            Current.MainWindow.Title = "Meerkat";
            Current.MainWindow.DataContext = meerkatAppViewModel;
        }
    }
}
