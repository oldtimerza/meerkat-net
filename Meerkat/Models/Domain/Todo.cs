namespace Meerkat.Models
{
    /// <summary>
    /// Represents a basic todo item with a done state and message
    /// </summary>
    public class Todo
    {
        public Todo(bool done, string message)
        {
            Done = done;
            Message = message;
        }

        public bool Done { get; set; }
        public string Message { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Todo && ((Todo)obj).Done.Equals(Done) && ((Todo)obj).Message.Equals(Message);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
