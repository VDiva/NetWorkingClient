using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameData;
using NetWorking;
using NetWorking.Net;
using NetWorking.Tool;
using NetWorkingServer;
using Newtonsoft.Json;
using UnityEngine;

public class NetManager : SingletonClass<NetManager>
{
    private int _selfId;
    private Client _client;

    private List<Client> _clients;
    
    public static Action<byte[],Client> OnMessageAction;
    public static Action<Client> OnConnectToServerAction;
    public static Action<Client> OnDisConnectToServerAction;

    

    public void ConnectToServer(string ip,int port)
    {
        NetWorking<Message> netWorking = new NetWorking<Message>();
        OnMessageAction += OnMessage;
        _client = netWorking.NetAsClient(ip, port);
    }

    
    
    public void SenMessage(object data)
    {
        _client.SendMessage(GameTool.Serialization(data));
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
    
    
    public void OnMessage(byte[] data, object client)
    {
        Debug.Log("a");
        Data value = GameTool.DeSerialization<Data>(data);
        switch (value.MsgType)
        {
            case MsgType.AllocationIdmsg:
                Debug.Log(value.ID);
                _selfId = value.ID;
                break;
            case MsgType.AnimMsg:
                break;
            case MsgType.StringMsg:
                break;
            case MsgType.TransformMsg:
                break;
            case MsgType.JoinRoomMsg:
                break;
            case MsgType.JoinRandomRoomMsg:
                break;
            case MsgType.CreateRoomMsg:
                break;
            default:
                break;
        }

    }
    
    
}
