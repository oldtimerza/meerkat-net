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

        [TestInitialize]
        public void TestInitialize()
        {
            mockMeerkatApp = new Mock<IMeerkatApp>();
            todosViewModel = new TodosViewModel(mockMeerkatApp.Object);
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

        [TestMethod]
        public void ShouldCreateTodo()
        {
            string message = "todo message";

            todosViewModel.AddTodo.Execute(message);

            mockMeerkatApp.Verify(app => app.CreateTodo(new Todo(false, message)));
            Assert.AreEqual(todosViewModel.InsertText, "");
            Assert.AreEqual(todosViewModel.FocusInsertText, false);
        }

        [TestMethod]
        public void ShouldEnterInsertMode()
        {
            todosViewModel.EnterInsertMode.Execute(null);

            mockMeerkatApp.Verify(app => app.EnterInsert());
            Assert.AreEqual(todosViewModel.FocusInsertText, true);
        }
    }
}
