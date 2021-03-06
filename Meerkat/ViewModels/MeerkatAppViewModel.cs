﻿using Meerkat.Models;
using Meerkat.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;

namespace Meerkat.ViewModels
{
    public class MeerkatAppViewModel : ViewModelBase
    {
        private IStateTracker stateTracker;
        private ITodoTracker todoTracker;
        private DispatcherTimer dispatcherTimer;
        private bool focusInsertText;
        private ICommand addTodo;
        private ICommand enterInsertMode;
        private ICommand nextTodoItem;
        private ICommand previousTodoItem;
        private ICommand toggleTodo;
        private ICommand removeTodo;
        private ICommand toggleTodoIsActive;

        public MeerkatAppViewModel(IStateTracker stateTracker, ITodoTracker todoTracker, DispatcherTimer dispatcherTimer)
        {
            this.stateTracker = stateTracker;
            this.todoTracker = todoTracker;
            this.dispatcherTimer = dispatcherTimer;
            this.dispatcherTimer.Tick += new System.EventHandler(TimeChanged);
        }

        private void TimeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Todos");
            OnPropertyChanged("SelectedIndex");
        }

        public IReadOnlyCollection<TodoViewModel> Todos
        {
            get
            {
                List<TodoViewModel> todoViewModels = new List<TodoViewModel>();
                foreach(Todo todo in todoTracker.Todos)
                {
                    todoViewModels.Add(TodoViewModel.From(todo));
                }
                return todoViewModels;
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
                        todoTracker.SelectNextTodo();
                        OnPropertyChanged("SelectedIndex");
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
                        todoTracker.SelectPreviousTodo();
                        OnPropertyChanged("SelectedIndex");
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

        public ICommand ToggleTodoIsActive
        {
            get
            {
                if(toggleTodoIsActive == null)
                {
                    toggleTodoIsActive = new RelayCommand(p =>
                    {
                        todoTracker.ToggleActiveTimerForTodo(SelectedIndex);
                        OnPropertyChanged("Todos");
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return toggleTodoIsActive;
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
                return todoTracker.SelectedIndex;
            }
        }
    }
}
