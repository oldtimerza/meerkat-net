using Meerkat.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stateless;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace MeerkatTests
{
    [TestClass]
    public class MeerkatAppTest
    {
        private Mock<IRepository<Todo>> mockRepository;
        private MeerkatApp app;
        private StateMachine<State, Trigger> stateMachine;
        private Mock<DispatcherTimer> mockDispatcher;
        private List<Todo> todos;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IRepository<Todo>>();
            stateMachine = new StateMachine<State, Trigger>(State.INSERT);
            mockDispatcher = new Mock<DispatcherTimer>();
            string message = "Some todo message";
            Todo todo = new Todo(false, message);
            todos = new List<Todo> { todo };
            mockRepository.Setup(repository => repository.Get()).Returns(todos);
            app = new MeerkatApp(stateMachine, mockRepository.Object, mockDispatcher.Object);
        }

        [TestMethod]
        public void ShouldAddTodo()
        {
            string message = "Newly created todo";
            Todo todo = new Todo(false, message);

            app.CreateTodo(todo);

            Assert.AreEqual(State.NAVIGATION, app.CurrentState);
            Assert.IsNotNull(app.Todos);
            Assert.AreEqual(2, app.Todos.Count);
            Todo actualTodo = app.Todos.ElementAt(1);
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
            app = new MeerkatApp(insertStateMachine, mockRepository.Object, mockDispatcher.Object);

            app.EnterInsert();

            Assert.AreEqual(State.INSERT, app.CurrentState);
        }

        [TestMethod]
        public void ShouldToggleTheTodoAtIndex()
        {
            int index = 0;
            Todo todo = todos.ElementAt(index);
            bool todoPrevDoneState = todo.Done;

            app.ToggleTodo(index);

            Assert.AreEqual(todo.Done, !todoPrevDoneState);
        }

        [TestMethod]
        public void ShouldRemoveTodo()
        {

            int index = 0;

            app.RemoveTodo(index);

            mockRepository.Verify(repository => repository.Delete(index));
        }

        [TestMethod]
        public void ShouldUpdateProgress()
        {
            Todo doneTodo = new Todo(true, "Done todo");
            app.CreateTodo(doneTodo);


            double halfComplete = 0.5;
            Assert.AreEqual(halfComplete, app.Progress);
        }

        [TestMethod]
        public void ShouldSelectNextTodo()
        {
            Todo doneTodo = new Todo(true, "Done todo");
            app.CreateTodo(doneTodo);

            app.SelectNextTodo();

            Assert.AreEqual(1, app.SelectedIndex);
        }

        [TestMethod]
        public void ShouldSelectPreviousTodo()
        {
            Todo doneTodo = new Todo(true, "Done todo");
            app.CreateTodo(doneTodo);
            app.EnterInsert();
            app.CreateTodo(doneTodo);

            app.SelectPreviousTodo();
            app.SelectPreviousTodo();

            Assert.AreEqual(1, app.SelectedIndex);
        }
    }
}
