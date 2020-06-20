using Meerkat.Model;
using Meerkat.View;
using Meerkat.ViewModel;
using Ninject;
using Stateless;
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
            base.OnStartup(e);
            this.container = new StandardKernel();
            this.ConfigureContainer();
            this.ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel();
            container.Bind<IRepository<Todo>>().To<TodoRepository>();
            container.Bind<StateMachine<State, Model.Trigger>>().ToMethod<StateMachine<State, Model.Trigger>>(context => new StateMachine<State, Model.Trigger>(State.NAVIGATION));
            container.Bind<IMeerkatApp>().To<MeerkatApp>();
            container.Bind<TodosViewModel>().To<TodosViewModel>();
        }

        private void ComposeObjects()
        {
            ViewModelBase viewModel = container.Get<TodosViewModel>();
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Meerkat";
            Current.MainWindow.DataContext = viewModel;
        }
    }
}
