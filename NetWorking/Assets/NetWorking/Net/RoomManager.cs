using System;
using System.Collections.Concurrent;
using GameData;
using NetWorking.Component;
using NetWorking.Tool;
using NetWorkingServer;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using NetWorking.Tool;
namespace NetWorking.Net
{
    public class RoomManager: SingletonMono<RoomManager>
    {
        private int _roomID;
        private readonly ConcurrentDictionary<int, GameObject> _clients=new ConcurrentDictionary<int, GameObject>();
        private readonly ConcurrentQueue<Msg.Msg> _messageQueue=new ConcurrentQueue<Msg.Msg>();


        public int GetRoomID()
        {
            return _roomID;
        }

        public void AddMessage(Msg.Msg msg)
        {
            _messageQueue.Enqueue(msg);
        }


        private void Update()
        {
       
            while (_messageQueue.Count>0)
            {
                if (_messageQueue.TryDequeue(out Msg.Msg msg))
                {
                    OnMessage(msg,msg.Data,msg.Client);
                }
            }
        
        }

        private void OnMessage(Msg.Msg msg,Data data, Client client)
        {
            switch (data.RoomMsgType)
            {
                case RoomMsgType.JoinRandomRoomMsgSucceed:
                    var selfObj = Instantiate(Resources.Load<GameObject>("Cube"), Vector3.zero, Quaternion.identity);
                    if (!selfObj.TryGetComponent<NetWorkingID>(out NetWorkingID selfWorkingID))
                    {
                        selfWorkingID = selfObj.AddComponent<NetWorkingID>();
                    }

                    _roomID = data.RoomData.RoomID;
                    selfWorkingID.ID = NetManager.Instance.GetID();
                    _clients.TryAdd(NetManager.Instance.GetID(), selfObj);
                    break;
                case RoomMsgType.JoinRoomMsgSucceed:
                    break;
                case RoomMsgType.JoinRoomCallBack:
                    try
                    {
                        foreach (var item in msg.Data.PlayerDatas)
                        {
                            var targetObj = Instantiate(Resources.Load<GameObject>("Cube"), Vector3.zero, Quaternion.identity);
                            if (!targetObj.TryGetComponent<NetWorkingID>(out NetWorkingID targetWorkingID))
                            {
                                targetWorkingID = targetObj.AddComponent<NetWorkingID>();
                            }
                            targetWorkingID.ID = item.ID;
                            _clients.TryAdd(item.ID, targetObj);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        
                    }
                    break;
                case RoomMsgType.RoomTransformMsg:
                   
                    if (_clients.TryGetValue(data.PlayerData.ID,out GameObject obj))
                    {
                        if (obj.TryGetComponent<SynRoomTransfrom>(out SynRoomTransfrom synRoomTransfrom))
                        {
                            synRoomTransfrom.SynTransform(data);
                        }
                    }
                    break;
            }

        }
    }
}