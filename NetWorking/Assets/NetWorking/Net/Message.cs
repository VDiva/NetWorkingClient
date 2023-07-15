using GameData;
using NetWorking.Tool;
using NetWorkingServer;
using UnityEngine;
using NetWorking.Msg;

namespace NetWorking.Net
{
    public class Message: IMessage
    {
        public void OnClientAccpet(ref Client client)
        {
            
        }

        public void OnMessage(byte[] data, ref Client client)
        {
            
            Data d = GameTool.DeSerialization<Data>(data);
            NetManager.Instance.AddMessage(new Msg.Msg(d,client));
        }

        public void OnConnectToServer(ref Client client)
        {
            
        }

        public void OnDisConnectToServer(ref Client client)
        {
           
        }
    }
}