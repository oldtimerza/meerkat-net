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
        public void ShouldReturnAllTodosOnGet()
        {
            List<Todo> todos = todoRepository.get();

            Assert.AreEqual(todos.Count, 1);
        }

        [TestMethod]
        public void ShouldCreateTodo()
        {
            Todo todo = new Todo(false, "Some todo message");

            todoRepository.create(todo);
            List<Todo> todos = todoRepository.get();

            Assert.AreEqual(todos.Count, 2);
        }
    }
}
