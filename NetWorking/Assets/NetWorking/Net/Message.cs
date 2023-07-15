using GameData;
using NetWorking.Tool;
using NetWorkingServer;
using UnityEngine;

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
            switch (d.MsgType)
            {
                case MsgType.JoinRandomRoomMsgSucceed:
                    
                    Debug.Log("加入随机房间成功");
                    break;
                case MsgType.JoinRoomCallBack:
                    
                    break;
            }
        }

        public void OnConnectToServer(ref Client client)
        {
            
        }

        public void OnDisConnectToServer(ref Client client)
        {
           
        }
    }
}