using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalRClient.ClientSignalRThings;
using Chat_SignalR_Biznesowe.SignalRThings;
using System.Threading.Tasks;
using Moq;

namespace SignalRClient.Tests
{
    [TestClass]
    public class ClientTests
    {
        private Mock<IChatHubConnection> mock_client;
        private Mock<IServer> mock_server;
        

        [TestInitialize]
        public void Startup()
        {

            mock_server = new Mock<IServer>();           
            mock_client = new Mock<IChatHubConnection>();

            mock_server.Setup(m => m.StartServer("http://localhost:8080"));
            mock_client.Setup(m => m.ConnectToChatHub("http://localhost:8080"));
          
           }

        [TestMethod]
        public void CanUserMaQAuthenticate()
        {
            // Arrange

           
             mock_client.Setup(m => m.CanUserAuthenticate("MaQ", "qwerty")).Returns(true);          
       

            String username = "MaQ";
            String password = "qwerty";

            bool expected = true;

            bool actual = mock_client.Object.CanUserAuthenticate(username, password);


            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CanUserFakeAuthenticate()
        {
            // Arrange


               mock_client.Setup(m => m.CanUserAuthenticate("MaQ", "qwerty")).Returns(false);          


            String username = "Fake";
            String password = "FakePasswd";

            bool expected = false;

            bool actual = mock_client.Object.CanUserAuthenticate(username, password);


            Assert.AreEqual(expected, actual);
        }
    }
}
