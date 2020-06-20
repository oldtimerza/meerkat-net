using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meerkat.ViewModel.Command
{
    public interface INotifyExecutionCommand : ICommand
    {
        event CommandExecutedHandler Executed;
    }
}
