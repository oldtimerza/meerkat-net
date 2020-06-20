using Meerkat.Model;
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
        private State currentState;
        private Action<State> stateAccessor; 
        private Func<State> stateMutator;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IRepository<Todo>>();
            currentState = State.INSERT;
            stateAccessor = state => currentState = state;
            stateMutator = () => currentState;
            stateMachine = new StateMachine<State, Trigger>(stateMutator, stateAccessor);
            app = new MeerkatApp(stateMachine, mockRepository.Object);
        }

        [TestMethod]
        public void ShouldAddTodo()
        {
            string message = "Some todo message";
            Todo todo = new Todo(false, message);
            mockRepository.Setup(repository => repository.get()).Returns(new List<Todo> { todo });

            app.CreateTodo(todo);

            Assert.AreEqual(State.NAVIGATION, currentState);
            Assert.IsNotNull(app.Todos);
            Assert.AreEqual(1, app.Todos.Count);
            Todo actualTodo = app.Todos.First();
            Assert.AreEqual(todo, actualTodo);
        }

        [TestMethod]
        public void ShouldEnterNavigationMode()
        {
            app.EnterNavigation();

            Assert.AreEqual(State.NAVIGATION, currentState);
        }

        [TestMethod]
        public void ShouldEnterInsertModeFromNavigationMode()
        {
            currentState = State.NAVIGATION;
            StateMachine<State, Trigger> insertStateMachine = new StateMachine<State, Trigger>(stateMutator, stateAccessor);
            app = new MeerkatApp(insertStateMachine, mockRepository.Object);

            app.EnterInsert();

            Assert.AreEqual(State.INSERT, currentState);
        }
    }
}
