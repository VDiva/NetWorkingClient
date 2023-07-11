using NetWorkingServer;

namespace NetWorking.Net
{
    public class Message: IMessage
    {
        public void OnClientAccpet(ref Client client)
        {
            
        }

        public void OnMessage(byte[] data, ref Client client)
        {
            
        }

        public void OnConnectToServer(ref Client client)
        {
            
        }

        public void OnDisConnectToServer(ref Client client)
        {
           
        }
    }
}