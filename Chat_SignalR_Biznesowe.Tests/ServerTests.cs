using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Reflection;

using Chat_SignalR_Biznesowe;
using Chat_SignalR_Biznesowe.SignalRThings;
using System.Threading.Tasks;

namespace Chat_SignalR_Biznesowe.Tests
{
    [TestClass]
    public class ServerTests
    {
        private Server sut;

        [TestInitialize]
        public void Startup()
        {
            sut = new Server();
        }

        [TestMethod]
        [ExpectedException(typeof(System.MissingMemberException))]
        public void Test_Server_Started_With_Bad_IP()
        {
            sut.StartServer("bad_ip");
        }

        [TestMethod]       
        public void Test_Server_Started_With_Good_IP()
        {
            Task.Run(() => sut.StartServer("http://localhost:8080"));
        }

        [TestCleanup]
        public void Clear()
        {
            sut = null;
        }
    }
}
