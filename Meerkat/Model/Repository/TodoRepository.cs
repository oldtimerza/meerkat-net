using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    public class TodoRepository : IRepository<Todo>
    {
        List<Todo> todos;

        public TodoRepository()
        {
            todos = new List<Todo>();
        }

        public void create(Todo todo)
        {
            todos.Add(todo);
        }

        public List<Todo> get()
        {
            return todos;
        }
    }
}
