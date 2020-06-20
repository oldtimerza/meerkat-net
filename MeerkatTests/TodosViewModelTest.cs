using System;
using System.Text;
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
    }
}
