﻿using Meerkat.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeerkatTests
{
    [TestClass]
    public class MeerkatAppTest
    {
        private Mock<IRepository<Todo>> mockRepository;
        private MeerkatApp app;
        private StateMachine<State, Trigger> stateMachine;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IRepository<Todo>>();
            stateMachine = new StateMachine<State, Trigger>(State.INSERT);
            app = new MeerkatApp(stateMachine, mockRepository.Object);
        }

        [TestMethod]
        public void ShouldAddTodo()
        {
            string message = "Some todo message";
            Todo todo = new Todo(false, message);
            mockRepository.Setup(repository => repository.get()).Returns(new List<Todo> { todo });

            app.CreateTodo(todo);

            Assert.AreEqual(State.NAVIGATION, app.CurrentState);
            Assert.IsNotNull(app.Todos);
            Assert.AreEqual(1, app.Todos.Count);
            Todo actualTodo = app.Todos.First();
            Assert.AreEqual(todo, actualTodo);
        }

        [TestMethod]
        public void ShouldEnterNavigationMode()
        {
            app.EnterNavigation();

            Assert.AreEqual(State.NAVIGATION, app.CurrentState);
        }

        [TestMethod]
        public void ShouldEnterInsertModeFromNavigationMode()
        {
            StateMachine<State, Trigger> insertStateMachine = new StateMachine<State, Trigger>(State.NAVIGATION);
            app = new MeerkatApp(insertStateMachine, mockRepository.Object);

            app.EnterInsert();

            Assert.AreEqual(State.INSERT, app.CurrentState);
        }
    }
}
