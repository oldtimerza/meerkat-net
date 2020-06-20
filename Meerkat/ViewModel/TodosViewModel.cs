using Meerkat.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Meerkat.ViewModel
{
    public class TodosViewModel : ViewModelBase
    {
        private IMeerkatApp meerkatApp;
        private ICommand addTodo;

        public TodosViewModel(IMeerkatApp meerkatApp, ICommand addTodo)
        {
            this.meerkatApp = meerkatApp;
            this.addTodo = addTodo;
        }

        public IReadOnlyCollection<Todo> Todos
        {
            get
            {
                return meerkatApp.Todos;
            }
        }

        public bool IsInsertMode
        {
            get
            {
                return meerkatApp.CurrentState.Equals(State.INSERT);
            }
        }

        public ICommand AddTodo
        {
            get
            {
                return addTodo;
            }
        }

    }
}
