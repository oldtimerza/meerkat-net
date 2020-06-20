using Meerkat.Model;
using Meerkat.View;
using Meerkat.ViewModel;
using Meerkat.ViewModel.Command;
using Ninject;
using Stateless;
using System.Windows;
using System.Windows.Input;
using static Meerkat.ViewModel.Command.AddTodo;
using static Meerkat.ViewModel.Command.EnterInsertMode;

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
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel();
            container.Bind<IRepository<Todo>>().To<TodoRepository>();
            container.Bind<StateMachine<State, Model.Trigger>>().ToMethod<StateMachine<State, Model.Trigger>>(context => new StateMachine<State, Model.Trigger>(State.NAVIGATION));
            container.Bind<IMeerkatApp>().To<MeerkatApp>().InSingletonScope();
            container.Bind<INotifyExecutionCommand>().To<AddTodo>().WhenTargetHas<AddTodoNeeded>();
            container.Bind<INotifyExecutionCommand>().To<EnterInsertMode>().WhenTargetHas<EnterInsertModeNeeded>();
            container.Bind<ViewModelBase>().To<TodosViewModel>();
        }

        private void ComposeObjects()
        {
            ViewModelBase viewModel = container.Get<ViewModelBase>();
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Meerkat";
            Current.MainWindow.DataContext = viewModel;
        }
    }
}
