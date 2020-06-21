using System.Collections.Generic;
using System.Linq;

namespace Meerkat.Models
{
    public class TodoRepository : IRepository<Todo>
    {
        List<Todo> todos;

        public TodoRepository()
        {
            todos = new List<Todo>();
        }

        public void Create(Todo todo)
        {
            todos.Add(todo);
        }

        public void Delete(int id)
        {
            todos.RemoveAt(id);
        }

        public List<Todo> Get()
        {
            return todos;
        }

        public Todo Update(int id, Todo t)
        {
            Todo todo = todos.ElementAt(id);

            todo.Done = t.Done;
            todo.Message = t.Message;

            return todo;
        }

    }
}
