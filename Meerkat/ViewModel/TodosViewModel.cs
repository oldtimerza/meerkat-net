using Meerkat.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
