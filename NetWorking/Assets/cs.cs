using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class cs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetManager.instance.ConnectToServer("127.0.0.1",8888);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
