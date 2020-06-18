using Meerkat.Model.States;
using Meerkat.Model.Triggers;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    class VimStateMachine
    {
        private StateMachine<State, Trigger> vimMachine;
        public VimStateMachine()
        {
            vimMachine = new StateMachine<State, Trigger>(State.NAVIGATION);
        }
    }
}
