﻿using Meerkat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meerkat.ViewModel.Command
{
    public class AddTodo : INotifyExecutionCommand
    {
        public class AddTodoNeeded : Attribute { }

        public event EventHandler CanExecuteChanged;
        public event CommandExecutedHandler Executed;

        private IMeerkatApp meerkatApp;

        public AddTodo(IMeerkatApp meerkatApp)
        {
            this.meerkatApp = meerkatApp;
        }

        public bool CanExecute(object parameter)
        {
            return null != meerkatApp;
        }


        public void Execute(object parameter)
        {
            meerkatApp.CreateTodo(new Todo(false, (string)parameter));
            Executed?.Invoke(true);
        }
    }
}
