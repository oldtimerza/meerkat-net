namespace Meerkat.Models
{
    /// <summary>
    /// Tracks the state of a state machine for VIM style state changes 
    /// </summary>
    public interface IStateTracker
    {
        /// <summary>
        /// The current state that the state machine is in.
        /// </summary>
        State CurrentState { get; }

        /// <summary>
        /// Change the state machines state to insert mode.
        /// </summary>
        void EnterInsert();

        /// <summary>
        /// Change the state machines state to navigation mode.
        /// </summary>
        void EnterNavigation();
    }
}
