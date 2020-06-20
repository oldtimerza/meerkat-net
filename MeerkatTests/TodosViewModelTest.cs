using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.ViewModel;
using Meerkat.Model;
using Moq;
using System.Windows.Input;

namespace MeerkatTests
{
    [TestClass]
    public class TodosViewModelTest
    {
        private TodosViewModel todosViewModel;
        private Mock<IMeerkatApp> mockMeerkatApp;
        private Mock<ICommand> mockAddTodo;

        [TestInitialize]
        public void TestInitialize()
        {
            mockAddTodo = new Mock<ICommand>();
            mockMeerkatApp = new Mock<IMeerkatApp>();
            todosViewModel = new TodosViewModel(mockMeerkatApp.Object, mockAddTodo.Object);
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
