using Meerkat.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stateless;
using System;
using System.Linq;

namespace MeerkatTests
{
    [TestClass]
    public class MeerkatAppTest
    {
        private MeerkatApp app;
        private StateMachine<State, Trigger> stateMachine;
        private State currentState;
        private Action<State> stateAccessor; 
        private Func<State> stateMutator;

        [TestInitialize]
        public void TestInitialize()
        {
            currentState = State.INSERT;
            stateAccessor = state => currentState = state;
            stateMutator = () => currentState;
            stateMachine = new StateMachine<State, Trigger>(stateMutator, stateAccessor);
            app = new MeerkatApp(stateMachine);

        }

        [TestMethod]
        public void ShouldAddTodo()
        {
            string message = "Some todo message";
            Todo todo = new Todo() { done = false, message = message };

            app.CreateTodo(todo);

            Assert.AreEqual(State.NAVIGATION, currentState);
            Assert.IsNotNull(app.TodoList);
            Assert.AreEqual(app.TodoList.Count, 1);
            Todo actualTodo = app.TodoList.First();
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
            app = new MeerkatApp(insertStateMachine);
            app.EnterInsert();

            Assert.AreEqual(State.INSERT, currentState);
        }
    }
}
