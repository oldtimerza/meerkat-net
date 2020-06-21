using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.Model;

namespace MeerkatTests
{
    /// <summary>
    /// Summary description for TodoRepositoryTest
    /// </summary>
    [TestClass]
    public class TodoRepositoryTest
    {

        private TodoRepository todoRepository;
        private Todo todo;
        
        [TestInitialize]
        public void TestInitialize()
        {
            todoRepository = new TodoRepository();
            todo = new Todo(false, "Some todo message");
            todoRepository.Create(todo);
        }

        [TestMethod]
        public void ShouldCreateTodo()
        {
            List<Todo> todos = todoRepository.Get();

            Assert.AreEqual(todos.Count, 1);
            Todo onlyTodo = todos[0];
            Assert.AreEqual(todo, onlyTodo);
        }

        [TestMethod]
        public void ShouldUpdateTodo()
        {
            string newMessage = "Some new Message";

            Todo updatedTodo = todoRepository.Update(0, new Todo(true, newMessage));

            Assert.AreEqual(true, updatedTodo.Done);
            Assert.AreEqual(newMessage, updatedTodo.Message);
        }

        [TestMethod]
        public void ShouldDeleteTodo()
        {
            int index = 0;

            todoRepository.Delete(index);
            List<Todo> todos = todoRepository.Get();

            Assert.AreEqual(todos.Count, 0);
        }
    }
}
