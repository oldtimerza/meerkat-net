using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meerkat.ViewModel.Command;
using Moq;
using Meerkat.Model;

namespace MeerkatTests
{
    [TestClass]
    public class AddTodoTest
    {
        [TestMethod]
        public void ShouldAddTodo()
        {
            string message = "Some todo message";
            Mock<IMeerkatApp> mockMeerkatApp = new Mock<IMeerkatApp>();
            AddTodo addTodo = new AddTodo(mockMeerkatApp.Object);

            addTodo.Execute(message);

            Todo todo = new Todo(false, message);
            mockMeerkatApp.Verify(app => app.CreateTodo(todo));
        }
    }
}
