using System.Collections.Generic;

namespace Meerkat.Models
{
    /// <summary>
    /// Keeps track of a Todo instances and manipulating their done state
    /// </summary>
    public interface ITodoTracker
    {
        /// <summary>
        /// The list of maintained Todo items
        /// </summary>
        IReadOnlyCollection<Todo> Todos { get; }

        /// <summary>
        /// The ratio of done todos of the total todo list
        /// </summary>
        double Progress { get; }

        /// <summary>
        /// Create a new Todo item in the list
        /// </summary>
        /// <param name="todo">The new Todo item</param>
        void CreateTodo(Todo todo);

        /// <summary>
        /// Change the Done state of the Todo item.
        /// </summary>
        /// <param name="id">The id of the Todo item.</param>
        void ToggleTodo(int id);

        /// <summary>
        /// Remove a Todo item from the list of items
        /// </summary>
        /// <param name="id">The id of the Todo item to remove</param>
        void RemoveTodo(int id);
    }
}
