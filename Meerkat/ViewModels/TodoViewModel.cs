using Meerkat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meerkat.ViewModels
{
    public class TodoViewModel
    {
        public bool Done { get; set; }
        public string Message { get; private set; }

        public bool Active { get; private set; }

        public string TimeActive { get; private set; }

        public TodoViewModel(Todo todo)
        {
            this.Done = todo.Done;
            this.Message = todo.Message;
            this.Active = todo.Active;
            this.TimeActive = todo.TimeActive.ToString();
        }
    }
}
