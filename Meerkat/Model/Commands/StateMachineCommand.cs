using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model.Commands
{
    interface StateMachineCommand
    {
        void execute(TodoStateMachine todoStateMachine);
    }
}
