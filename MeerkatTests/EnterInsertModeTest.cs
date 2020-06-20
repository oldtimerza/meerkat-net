using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Meerkat.Model;
using Meerkat.ViewModel.Command;

namespace MeerkatTests
{
    [TestClass]
    public class EnterInsertModeTest
    {
        [TestMethod]
        public void ShouldCallEnterInsert()
        {
            Mock<IMeerkatApp> meerkatApp = new Mock<IMeerkatApp>();
            EnterInsertMode enterInsertMode = new EnterInsertMode(meerkatApp.Object);

            enterInsertMode.Execute(null);

            meerkatApp.Verify(app => app.EnterInsert());
        }
    }
}
