using Meerkat.Model;
using Meerkat.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using static Meerkat.ViewModel.Command.AddTodo;
using static Meerkat.ViewModel.Command.EnterInsertMode;

namespace Meerkat.ViewModel
{
    public class TodosViewModel : ViewModelBase
    {
        private IMeerkatApp meerkatApp;
        private string insertText;
        private bool focusInsertText;

        public TodosViewModel(IMeerkatApp meerkatApp, [AddTodoNeeded] INotifyExecutionCommand addTodo, [EnterInsertModeNeeded] INotifyExecutionCommand enterInsertMode)
        {
            this.meerkatApp = meerkatApp;
            AddTodo = addTodo;
            EnterInsertMode = enterInsertMode;
            addTodo.Executed += AddTodoExecuted;
            enterInsertMode.Executed += EnterInsertExecuted;
        }


        public IReadOnlyCollection<Todo> Todos
        {
            get
            {
                return meerkatApp.Todos;
            }
        }

        public bool IsInsertMode
        {
            get
            {
                return meerkatApp.CurrentState == State.INSERT;
            }
        }

        public ICommand AddTodo { get; }

        public ICommand EnterInsertMode { get; }

        public string InsertText
        {
            get
            {
                return insertText;
            }

            set
            {
                insertText = value;
                OnPropertyChanged("InsertText");
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

        private void AddTodoExecuted(bool isSuccessful)
        {
            InsertText = "";
            FocusInsertText = false;
            if (isSuccessful)
            {
                OnPropertyChanged("IsInsertMode");
                OnPropertyChanged("Todos");
            }
        }

        private void EnterInsertExecuted(bool isSuccessful)
        {
            FocusInsertText = true;
            if (isSuccessful)
            {
                OnPropertyChanged("IsInsertMode");
                OnPropertyChanged("Todos");
            }
        }

    }
}
