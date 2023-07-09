using NetWorkingServer;

namespace NetWorking.Net
{
    public class Message: IMessage
    {
        public void OnClientAccpet(object client)
        {
           
        }

        public void OnMessage(byte[] data, object client)
        {
           NetManager.OnMessageAction?.Invoke(data,client as Client<Message>);
        }

        public void OnConnectToServer(object client)
        {
            NetManager.OnConnectToServerAction?.Invoke(client as Client<Message>);
        }

        public void OnDisConnectToServer(object client)
        {
            NetManager.OnDisConnectToServerAction?.Invoke(client as Client<Message>);
        }
    }
}