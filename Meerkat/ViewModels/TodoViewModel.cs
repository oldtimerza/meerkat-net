using Meerkat.Models;
using System;

namespace Meerkat.ViewModels
{
    public class TodoViewModel
    {
        public bool Done { get; set; }

        public string Message { get; private set; }

        public bool Active { get; private set; }

        public string TimeActive { get; private set; }

        public static TodoViewModel From(Todo todo)
        {
            TodoViewModel todoViewModel = new TodoViewModel();
            todoViewModel.Done = todo.Done;
            todoViewModel.Message = todo.Message;
            todoViewModel.Active = todo.Active;
            todoViewModel.TimeActive = todo.TimeActive.ToString();
            return todoViewModel;
        }
    }
}
