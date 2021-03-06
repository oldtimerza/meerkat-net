using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.ViewModels;
using Meerkat.Models;
using Moq;
using System.Windows.Threading;

namespace MeerkatTests
{
    [TestClass]
    public class TodosViewModelTest
    {
        private MeerkatAppViewModel todosViewModel;
        private Mock<IStateTracker> mockStateTracker;
        private Mock<ITodoTracker> mockTodoTracker;
        private Mock<DispatcherTimer> mockDispatcher;

        [TestInitialize]
        public void TestInitialize()
        {
            mockStateTracker = new Mock<IStateTracker>();
            mockTodoTracker = new Mock<ITodoTracker>();
            mockDispatcher = new Mock<DispatcherTimer>();
            todosViewModel = new MeerkatAppViewModel(mockStateTracker.Object, mockTodoTracker.Object, mockDispatcher.Object);
        }

        [TestMethod]
        public void ShouldConvertTodosToViewModel()
        {
            List<Todo> expectedTodos = new List<Todo>();
            expectedTodos.Add(new Todo(false, "Test todo"));
            mockTodoTracker.Setup(tracker => tracker.Todos).Returns(expectedTodos.AsReadOnly());

            IReadOnlyCollection<TodoViewModel> actualTodos = todosViewModel.Todos;

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
            todosViewModel.NextTodoItem.Execute(null);

            mockTodoTracker.Verify(tracker => tracker.SelectNextTodo());
        }

        [TestMethod]
        public void ShouldSelectPreviousIndexWithWrap()
        {
            todosViewModel.PreviousTodoItem.Execute(null);

            mockTodoTracker.Verify(tracker => tracker.SelectPreviousTodo());
        }

        [TestMethod]
        public void ShouldRemoveTodo()
        {
            todosViewModel.RemoveTodo.Execute(null);

            mockTodoTracker.Verify(tracker => tracker.RemoveTodo(todosViewModel.SelectedIndex));
        }

        [TestMethod]
        public void ShouldHaveProgress()
        {
            double progress = 0.5;
            mockTodoTracker.Setup(tracker => tracker.Progress).Returns(progress);

            double actualProgress = todosViewModel.Progress;

            Assert.AreEqual(progress, actualProgress);
        }
    }
}
