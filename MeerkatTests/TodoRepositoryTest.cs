﻿using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.Models;

namespace MeerkatTests
{
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
            IEnumerable<Todo> todos = todoRepository.Get();

            Assert.AreEqual(todos.Count(), 1);
            Todo onlyTodo = todos.ElementAt(0);
            Assert.AreEqual(todo, onlyTodo);
        }

        [Ignore]
        [TestMethod]
        public void ShouldUpdateTodo()
        {
            /*This test is being ignored until the repository is correctly setup*/
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
            IEnumerable<Todo> todos = todoRepository.Get();

            Assert.AreEqual(todos.Count(), 0);
        }
    }
}
