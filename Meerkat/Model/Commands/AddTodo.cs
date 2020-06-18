using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model.Commands
{
    class AddTodo : StateMachineCommand
    {
        private Todo item;

        public AddTodo(Todo item)
        {
            this.item = item;
        }

        public void Execute(TodoStateMachine todoStateMachine)
        {
            todoStateMachine.AddTodoItem(item);
        }
    }
}
