using Stateless;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Meerkat.Models
{
    public class MeerkatApp : IStateTracker, ITodoTracker
    {
        private StateMachine<State, Trigger> stateMachine;
        private IList<Todo> todos;
        private IRepository<Todo> repository;
        private StateMachine<State, Trigger>.TriggerWithParameters<Todo> createTodoTrigger;
        private double progress;

        public MeerkatApp(StateMachine<State, Trigger> stateMachine, IRepository<Todo> repository, DispatcherTimer dispatcherTimer)
        {
            this.todos = new List<Todo>();
            this.repository = repository;

            //setup statemachine
            this.stateMachine = stateMachine;
            createTodoTrigger = this.stateMachine.SetTriggerParameters<Todo>(Trigger.CREATE_TODO);
            stateMachine.Configure(State.INSERT).Permit(Trigger.EXIT_EDITOR, State.NAVIGATION);
            stateMachine.Configure(State.INSERT).Permit(Trigger.CREATE_TODO, State.NAVIGATION);
            stateMachine.Configure(State.NAVIGATION)
                .OnEntryFrom(createTodoTrigger, todo => AddTodoItem(todo))
                .Permit(Trigger.ENTER_EDITOR, State.INSERT);

            //setup dispatcher 
            dispatcherTimer.Tick += new EventHandler(Tick);
        }

        public virtual IReadOnlyCollection<Todo> Todos
        {
            get { return  new ReadOnlyCollection<Todo>(todos); }
        }
        
        public virtual State CurrentState
        {
            get
            {
                return stateMachine.State;
            }
        }

        public virtual double Progress
        {
            get
            {
                return this.progress;
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
            todos.Add(item);
            UpdateProgress();
        }

        public void ToggleTodo(int index)
        {
            Todo todo = todos.ElementAt(index);
            todo.ToggleDone();
            repository.Update(index, todo);
            UpdateProgress();
        }

        public void RemoveTodo(int index)
        {
            todos.RemoveAt(index);
            repository.Delete(index);
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            double doneTodosCount = todos.Count(todo => todo.Done);
            double totalCount = todos.Count();
            progress = doneTodosCount / totalCount;
        }

        private void Tick(object sender, EventArgs e)
        {
            foreach (Todo item in todos)
            {
                item.Tick();
            }
        }

        public void ToggleActiveTimerForTodo(int index)
        {
            Todo todo = todos.ElementAt(index);
            todo.ToggleActiveTimer();
        }
    }
}
