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

        public IEnumerable<Todo> Get()
        {
            return todos;
        }

        public Todo Update(int id, Todo t)
        {
            //TODO: this needs to be cleaned up, perhaps a separate model for storage in future.
            //For now It's being ignored.
            Todo todo = todos.ElementAt(id);

            return todo;
        }

    }
}
