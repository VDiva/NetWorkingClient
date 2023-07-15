using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameData;
using NetWorking;
using NetWorking.Msg;
using NetWorking.Net;
using NetWorking.Tool;
using NetWorkingServer;
using Newtonsoft.Json;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class NetManager : SingletonMono<NetManager>
{
    public int _selfId;
    private Client _client;
    
    private ConcurrentDictionary<int,GameObject> _clients;

    private ConcurrentQueue<Msg> _messageQueue;


    public static Action<Data,Client> OnMessageAction;
    public static Action<Client> OnConnectToServerAction;
    public static Action<Client> OnDisConnectToServerAction;

    

    public void ConnectToServer(string ip,int port)
    {
        _messageQueue = new ConcurrentQueue<Msg>();
        _clients = new ConcurrentDictionary<int, GameObject>();
        NetWorking<Message> netWorking = new NetWorking<Message>();
        
        _client = netWorking.NetAsClient(ip, port);
    }


    public void AddMessage(Msg msg)
    {
        _messageQueue.Enqueue(msg);
    }
    
    public void SenMessage(object data)
    {
        _client.SendMessage(GameTool.Serialization(data));
    }

    public bool IsLocal(int ID)
    {
        return ID == _selfId;
    }

    public int GetID()
    {
        return _selfId;
    }

    public bool IsOnline()
    {
        if (_client!=null)
        {
            if (_client.socket.Connected)
            {
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
       
        while (_messageQueue.Count>0)
        {
            if (_messageQueue.TryDequeue(out Msg msg))
            {
                OnMessage(msg,msg.Data,msg.Client);
            }
        }
        
    }

    private void OnMessage(Msg msg,Data data, Client client)
    {
       
        
        switch (data.MsgType)
        {
            case MsgType.AllocationIdmsg:
                _selfId = data.ID;
                break;
            case MsgType.RoomMsg:
                RoomManager.Instance.AddMessage(msg);
                break;
            case MsgType.LobbyMsg:
                break;
        }

    }
    
    
}
