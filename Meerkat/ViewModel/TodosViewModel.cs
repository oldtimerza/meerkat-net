using Meerkat.Model;
using Meerkat.ViewModel.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace Meerkat.ViewModel
{
    public class TodosViewModel : ViewModelBase
    {
        private IMeerkatApp meerkatApp;
        private string insertText;
        private bool focusInsertText;
        private int selectedIndex;
        private ICommand addTodo;
        private ICommand enterInsertMode;

        public TodosViewModel(IMeerkatApp meerkatApp)
        {
            this.meerkatApp = meerkatApp;
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

        public ICommand AddTodo {
            get
            {
                if(addTodo == null)
                {
                    addTodo = new RelayCommand(p => {
                        meerkatApp.CreateTodo(new Todo(false, (string)p));
                        InsertText = "";
                        FocusInsertText = false;
                        OnPropertyChanged("IsInsertMode");
                        OnPropertyChanged("Todos");
                    },
                    p => meerkatApp.CurrentState == State.INSERT);
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
                        meerkatApp.EnterInsert();
                        FocusInsertText = true;
                        OnPropertyChanged("IsInsertMode");
                    },
                    p => meerkatApp.CurrentState == State.NAVIGATION);
                }
                return enterInsertMode;
            }
        }

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
