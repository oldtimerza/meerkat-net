using Meerkat.Model;
using System.Collections.Generic;

namespace Meerkat.ViewModel
{
    public class TodosViewModel : ViewModelBase
    {
        private IMeerkatApp meerkatApp;

        public TodosViewModel(IMeerkatApp meerkatApp)
        {
            this.meerkatApp = meerkatApp;
        }

        public IReadOnlyCollection<Todo> Todos
        {
            get
            {
                return meerkatApp.Todos;
            }
        }
    }
}
