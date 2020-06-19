using Stateless;
using System.Collections.Generic;

namespace Meerkat.Model
{
    public class MeerkatApp
    {
        private StateMachine<State, Trigger> stateMachine;
        private List<Todo> todoList;
        private StateMachine<State, Trigger>.TriggerWithParameters<Todo> createTodoTrigger;

        public MeerkatApp(StateMachine<State, Trigger> stateMachine)
        {
            this.stateMachine = stateMachine;
            todoList = new List<Todo>();
            createTodoTrigger = this.stateMachine.SetTriggerParameters<Todo>(Trigger.CREATE_TODO);
            stateMachine.Configure(State.INSERT).Permit(Trigger.EXIT_EDITOR, State.NAVIGATION);
            stateMachine.Configure(State.INSERT).Permit(Trigger.CREATE_TODO, State.NAVIGATION);
            stateMachine.Configure(State.NAVIGATION)
                .OnEntryFrom(createTodoTrigger, todo => AddTodoItem(todo))
                .Permit(Trigger.ENTER_EDITOR, State.INSERT);
        }

        public IReadOnlyCollection<Todo> TodoList
        {
            get { return todoList.AsReadOnly(); }
        }

        public void CreateTodo(Todo todo)
        {
            stateMachine.Fire(createTodoTrigger, todo);
        }

        public void EnterNavigation()
        {
            stateMachine.Fire(Trigger.EXIT_EDITOR);
        }
    
        public void EnterInsert()
        {
            stateMachine.Fire(Trigger.ENTER_EDITOR);
        }

        private void AddTodoItem(Todo item)
        {
            todoList.Add(item);
        }
    }
}
