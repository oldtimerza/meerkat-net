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

            todoRepository.create(todo);
            List<Todo> todos = todoRepository.get();

            Assert.AreEqual(todos.Count, 1);
            Todo onlyTodo = todos[0];
            Assert.AreEqual(todo, onlyTodo);
        }
    }
}
