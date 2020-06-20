using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.ViewModel;
using Meerkat.Model;
using Moq;
using System.Windows.Input;
using System.ComponentModel;
using Meerkat.ViewModel.Command;

namespace MeerkatTests
{
    [TestClass]
    public class TodosViewModelTest
    {
        private TodosViewModel todosViewModel;
        private Mock<IMeerkatApp> mockMeerkatApp;
        private Mock<INotifyExecutionCommand> mockAddTodo;
        private Mock<INotifyExecutionCommand> mockEnterInsertMode;

        [TestInitialize]
        public void TestInitialize()
        {
            mockAddTodo = new Mock<INotifyExecutionCommand>();
            mockEnterInsertMode = new Mock<INotifyExecutionCommand>();
            mockMeerkatApp = new Mock<IMeerkatApp>();
            todosViewModel = new TodosViewModel(mockMeerkatApp.Object, mockAddTodo.Object, mockEnterInsertMode.Object);
        }

        [TestMethod]
        public void ShouldHaveModelsTodos()
        {
            List<Todo> expectedTodos = new List<Todo>();
            expectedTodos.Add(new Todo(false, "Test todo"));
            mockMeerkatApp.Setup(meerkatApp => meerkatApp.Todos).Returns(expectedTodos.AsReadOnly());

            IReadOnlyCollection<Todo> actualTodos = todosViewModel.Todos;

            Assert.AreEqual(1, actualTodos.Count);
        }

        [TestMethod]
        public void ShouldHaveIsInsertModeFalseWhenInNavigationMode()
        {
            mockMeerkatApp.Setup(app => app.CurrentState).Returns(State.NAVIGATION);

            Assert.IsFalse(todosViewModel.IsInsertMode);
        }
    }
}
