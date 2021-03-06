using System;

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

        public bool Done { get; private set; }
        public string Message { get; private set; }

        public bool Active { get; private set; }

        public TimeSpan TimeActive { get; set; }

        public void ToggleDone()
        {
            Done = !Done;
            if(Active)
            {
                Active = false;
            }
        }

        public void ToggleActiveTimer()
        {
            if(!Done)
            {
                Active = !Active;
            }
        }

        public void Tick()
        {
            if(Active)
            {
                TimeActive = TimeActive.Add(new TimeSpan(0, 0, 1));
            }
        }

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
