namespace Meerkat.Models
{
    /// <summary>
    /// The triggers that will cause a state change in the state machine.
    /// </summary>
    public enum Trigger
    {
        CREATE_TODO,
        ENTER_EDITOR,
        EXIT_EDITOR
    }
}
