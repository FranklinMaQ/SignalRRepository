using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat_SignalR_Biznesowe;

namespace SignalRUnitTests
{
    [TestClass]
    public class SerwerTest
    {
        private MainWindow sut;

        [TestInitialize]
        public void Startup()
        {
            sut = new MainWindow();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
