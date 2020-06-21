﻿using Meerkat.Model;
using Meerkat.ViewModel.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace Meerkat.ViewModel
{
    public class TodosViewModel : ViewModelBase
    {
        private IStateTracker stateTracker;
        private ITodoTracker todoTracker;
        private string insertText;
        private bool focusInsertText;
        private int selectedIndex;
        private ICommand addTodo;
        private ICommand enterInsertMode;
        private ICommand nextTodoItem;

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

        public ICommand AddTodo {
            get
            {
                if(addTodo == null)
                {
                    addTodo = new RelayCommand(p => {
                        todoTracker.CreateTodo(new Todo(false, (string)p));
                        FocusInsertText = false;
                        OnPropertyChanged("IsInsertMode");
                        OnPropertyChanged("Todos");
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
                    enterInsertMode = new RelayCommand(p => {
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
                        SelectedIndex = (SelectedIndex + 1) % todoTracker.Todos.Count;
                    },
                    p => stateTracker.CurrentState == State.NAVIGATION);
                }
                return nextTodoItem;
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
