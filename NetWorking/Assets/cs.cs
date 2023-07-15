using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using NetWorking.Net;
using UnityEngine;

public class cs : MonoBehaviour
{
    private void Awake()
    {
        
        NetManager.instance.ConnectToServer("127.0.0.1",8888);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        NetManager.instance.SenMessage(new Data{MsgType = MsgType.JoinRandomRoomMsg});
        // NetManager.instance.SenMessage(new Data{MsgType = MsgType.TransformMsg});
        Debug.Log("daaaaaa");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     
        //     NetManager.instance.SenMessage(new Data{MsgType = MsgType.TransformMsg});
        // }
    }
}
