using Meerkat.Model;
using Ninject;
using System.Collections.Generic;

namespace Meerkat.ViewModel
{
    class TodosViewModel : ViewModelBase
    {
        [Inject]
        private MeerkatApp model;

        public IReadOnlyCollection<Todo> Todos
        {
            get
            {
                return model.TodoList;
            }
        }
    }
}
