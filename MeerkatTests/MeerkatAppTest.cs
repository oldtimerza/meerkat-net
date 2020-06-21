using Meerkat.Models;
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
            mockRepository.Setup(repository => repository.Get()).Returns(new List<Todo> { todo });

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

        [TestMethod]
        public void ShouldToggleTheTodoAtIndex()
        {
            int index = 0;
            Todo todo = new Todo(false, "Some todo");
            mockRepository.Setup(repository => repository.Get()).Returns(new List<Todo> { todo });

            app.ToggleTodo(index);

            mockRepository.Verify(repository => repository.Update(index, new Todo(true, todo.Message)));
        }

        [TestMethod]
        public void ShouldRemoveTodo()
        {
            int index = 0;

            app.RemoveTodo(index);

            mockRepository.Verify(repository => repository.Delete(index));
        }
    }
}
