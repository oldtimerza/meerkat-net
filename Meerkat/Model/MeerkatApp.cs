using Stateless;
using System.Collections.Generic;

namespace Meerkat.Model
{
    public class MeerkatApp : IStateTracker, ITodoTracker
    {
        private StateMachine<State, Trigger> stateMachine;
        private IRepository<Todo> repository;
        private StateMachine<State, Trigger>.TriggerWithParameters<Todo> createTodoTrigger;

        public MeerkatApp(StateMachine<State, Trigger> stateMachine, IRepository<Todo> repository)
        {
            this.stateMachine = stateMachine;
            this.repository = repository;
            createTodoTrigger = this.stateMachine.SetTriggerParameters<Todo>(Trigger.CREATE_TODO);
            stateMachine.Configure(State.INSERT).Permit(Trigger.EXIT_EDITOR, State.NAVIGATION);
            stateMachine.Configure(State.INSERT).Permit(Trigger.CREATE_TODO, State.NAVIGATION);
            stateMachine.Configure(State.NAVIGATION)
                .OnEntryFrom(createTodoTrigger, todo => AddTodoItem(todo))
                .Permit(Trigger.ENTER_EDITOR, State.INSERT);
        }

        public virtual IReadOnlyCollection<Todo> Todos
        {
            get { return repository.Get().AsReadOnly(); }
        }
        
        public virtual State CurrentState
        {
            get
            {
                return stateMachine.State;
            }
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
            repository.Create(item);
        }

        public void ToggleTodo(int index)
        {
            Todo todo = repository.Get()[index];
            repository.Update(index, new Todo(!todo.Done, todo.Message));
        }

        public void RemoveTodo(int index)
        {
            repository.Delete(index);
        }
    }
}
