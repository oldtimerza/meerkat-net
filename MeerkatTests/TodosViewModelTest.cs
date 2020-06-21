using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.ViewModel;
using Meerkat.Model;
using Moq;

namespace MeerkatTests
{
    [TestClass]
    public class TodosViewModelTest
    {
        private TodosViewModel todosViewModel;
        private Mock<IStateTracker> mockStateTracker;
        private Mock<ITodoTracker> mockTodoTracker;

        [TestInitialize]
        public void TestInitialize()
        {
            mockStateTracker = new Mock<IStateTracker>();
            mockTodoTracker = new Mock<ITodoTracker>();
            todosViewModel = new TodosViewModel(mockStateTracker.Object, mockTodoTracker.Object);
        }

        [TestMethod]
        public void ShouldHaveModelsTodos()
        {
            List<Todo> expectedTodos = new List<Todo>();
            expectedTodos.Add(new Todo(false, "Test todo"));
            mockTodoTracker.Setup(tracker => tracker.Todos).Returns(expectedTodos.AsReadOnly());

            IReadOnlyCollection<Todo> actualTodos = todosViewModel.Todos;

            Assert.AreEqual(1, actualTodos.Count);
        }

        [TestMethod]
        public void ShouldHaveIsInsertModeFalseWhenInNavigationMode()
        {
            mockStateTracker.Setup(tracker => tracker.CurrentState).Returns(State.NAVIGATION);

            Assert.IsFalse(todosViewModel.IsInsertMode);
        }

        [TestMethod]
        public void ShouldCreateTodo()
        {
            string message = "todo message";

            todosViewModel.AddTodo.Execute(message);

            mockTodoTracker.Verify(tracker => tracker.CreateTodo(new Todo(false, message)));
            Assert.AreEqual(todosViewModel.FocusInsertText, false);
        }

        [TestMethod]
        public void ShouldEnterInsertMode()
        {
            todosViewModel.EnterInsertMode.Execute(null);

            mockStateTracker.Verify(app => app.EnterInsert());
            Assert.AreEqual(todosViewModel.FocusInsertText, true);
        }

        [TestMethod]
        public void ShouldSelectNextIndex()
        {
            List<Todo> todos = new List<Todo>();
            todos.Add(new Todo(false, "doesnt matter"));
            todos.Add(new Todo(false,"doesnt matter 2: electric boogaloo"));
            mockTodoTracker.Setup(tracker => tracker.Todos).Returns(todos.AsReadOnly);
            int currentIndex = todosViewModel.SelectedIndex;

            todosViewModel.NextTodoItem.Execute(null);

            Assert.AreEqual(currentIndex + 1, todosViewModel.SelectedIndex);
        }

        [TestMethod]
        public void ShouldSelectPreviousIndexWithWrap()
        {
            List<Todo> todos = new List<Todo>();
            todos.Add(new Todo(false, "doesnt matter"));
            todos.Add(new Todo(false,"doesnt matter 2: electric boogaloo"));
            todos.Add(new Todo(false,"doesnt matter 3: revenge of the matters"));
            mockTodoTracker.Setup(tracker => tracker.Todos).Returns(todos.AsReadOnly);
            int currentIndex = todosViewModel.SelectedIndex;

            todosViewModel.PreviousTodoItem.Execute(null);

            Assert.AreEqual(2, todosViewModel.SelectedIndex);
        }

        [TestMethod]
        public void ShouldRemoveTodo()
        {
            todosViewModel.RemoveTodo.Execute(null);

            mockTodoTracker.Verify(tracker => tracker.RemoveTodo(todosViewModel.SelectedIndex));
        }
    }
}
