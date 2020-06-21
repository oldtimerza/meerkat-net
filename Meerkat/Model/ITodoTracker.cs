using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    public interface ITodoTracker
    {
        IReadOnlyCollection<Todo> Todos { get; }
        void CreateTodo(Todo todo);
        void ToggleTodo(int index);
    }
}
