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
        
        [TestInitialize]
        public void TestInitialize()
        {
            todoRepository = new TodoRepository();
        }

        [TestMethod]
        public void ShouldCreateTodo()
        {
            Todo todo = new Todo(false, "Some todo message");

            todoRepository.Create(todo);
            List<Todo> todos = todoRepository.Get();

            Assert.AreEqual(todos.Count, 1);
            Todo onlyTodo = todos[0];
            Assert.AreEqual(todo, onlyTodo);
        }

        [TestMethod]
        public void ShouldUpdateTodo()
        {
            Todo todo = new Todo(false, "Some todo message");
            string newMessage = "Some new Message";

            todoRepository.Create(todo);
            Todo updatedTodo = todoRepository.Update(0, new Todo(true, newMessage));

            Assert.AreEqual(true, updatedTodo.Done);
            Assert.AreEqual(newMessage, updatedTodo.Message);
        }
    }
}
