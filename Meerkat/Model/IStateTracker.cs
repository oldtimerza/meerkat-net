using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    public interface IStateTracker
    {
        State CurrentState { get; }
        void EnterInsert();
        void EnterNavigation();
    }
}
