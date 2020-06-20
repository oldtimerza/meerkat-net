﻿namespace Meerkat.Model
{
    public class Todo
    {
        private bool done;
        private string message;

        public Todo(bool done, string message)
        {
            this.done = done;
            this.message = message;
        }
        public bool Done { get { return done; } set { this.done = value; } }
        public string Message { get { return message; } set { this.message = value; } }
    }
}
