using Meerkat.Models;
using Meerkat.ViewModels.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Meerkat.ViewModels
{
    public class TodosViewModel : ViewModelBase
    {
        private IStateTracker stateTracker;
        private ITodoTracker todoTracker;
        private bool focusInsertText;
        private int selectedIndex;
        private ICommand addTodo;
        private ICommand enterInsertMode;
        private ICommand nextTodoItem;
        private ICommand previousTodoItem;
        private ICommand toggleTodo;
        private ICommand removeTodo;

        public TodosViewModel(IStateTracker stateTracker, ITodoTracker todoTracker)
        {
            this.stateTracker = stateTracker;
            this.todoTracker = todoTracker;
        }


        public IReadOnlyCollection<Todo> Todos
        {
            get
            {
                return todoTracker.Todos;
            }
        }

        public bool IsInsertMode
        {
            get
            {
                return stateTracker.CurrentState == State.INSERT;
            }
        }

        public double Progress
        {
            get
            {
                return todoTracker.Progress;
            }
        }

        public ICommand AddTodo {
            get
            {
                if(addTodo == null)
                {
                    addTodo = new RelayCommand(p => 
                    {
                        todoTracker.CreateTodo(new Todo(false, (string)p));
                        FocusInsertText = false;
                        OnPropertyChanged("IsInsertMode");
                        OnPropertyChanged("Todos");
                        OnPropertyChanged("Progress");

                    },
                    p => stateTracker.CurrentState == State.INSERT);
                }
                return addTodo;
            }
        }

        public ICommand EnterInsertMode {
            get
            {
                if(enterInsertMode == null)
                {
                    enterInsertMode = new RelayCommand(p => 
                    {
                        stateTracker.EnterInsert();
                        FocusInsertText = true;
                        OnPropertyChanged("IsInsertMode");
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return enterInsertMode;
            }
        }

        public ICommand NextTodoItem
        {
            get
            {
                if(nextTodoItem == null)
                {
                    nextTodoItem = new RelayCommand(p =>
                    {
                        SelectedIndex = Utilities.Math.Mod(SelectedIndex + 1, todoTracker.Todos.Count);
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return nextTodoItem;
            }
        }

        public ICommand PreviousTodoItem
        {
            get
            {
                if(previousTodoItem == null)
                {
                    previousTodoItem = new RelayCommand(p =>
                    {
                        SelectedIndex = Utilities.Math.Mod(SelectedIndex - 1, todoTracker.Todos.Count);
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return previousTodoItem;
            }
        }
        
        public ICommand ToggleTodo
        {
            get
            {
                if(toggleTodo == null)
                {
                    toggleTodo = new RelayCommand(p =>
                    {
                        todoTracker.ToggleTodo(SelectedIndex);
                        OnPropertyChanged("Todos");
                        OnPropertyChanged("Progress");
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return toggleTodo;
            }
        }

        public ICommand RemoveTodo
        {
            get
            {
                if(removeTodo == null)
                {
                    removeTodo = new RelayCommand(p =>
                    {
                        todoTracker.RemoveTodo(SelectedIndex);
                        OnPropertyChanged("Todos");
                        OnPropertyChanged("Progress");
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return removeTodo;
            }
        }

        public bool FocusInsertText
        {
            get
            {
                return focusInsertText;
            }

            set
            {
                focusInsertText = value;
                OnPropertyChanged("FocusInsertText");
            }
        }

        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
    }
}
