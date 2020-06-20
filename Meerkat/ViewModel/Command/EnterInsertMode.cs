using Meerkat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meerkat.ViewModel.Command
{
    public class EnterInsertMode : INotifyExecutionCommand
    {
        public class EnterInsertModeNeeded : Attribute { }

        private IMeerkatApp meerkatApp;

        public event EventHandler CanExecuteChanged;
        public event CommandExecutedHandler Executed;

        public EnterInsertMode(IMeerkatApp meerkatApp)
        {
            this.meerkatApp = meerkatApp;
        }

        public bool CanExecute(object parameter)
        {
            return null != meerkatApp && meerkatApp.CurrentState != State.INSERT;
        }

        public void Execute(object parameter)
        {
            meerkatApp.EnterInsert();
            Executed?.Invoke(true);
        }
    }
}
