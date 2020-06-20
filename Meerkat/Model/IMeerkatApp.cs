using System.Collections.Generic;

namespace Meerkat.Model
{
    public interface IMeerkatApp
    {
        IReadOnlyCollection<Todo> Todos { get; }
        State CurrentState { get; } 

        void CreateTodo(Todo todo);
        void EnterInsert();
        void EnterNavigation();
    }
}