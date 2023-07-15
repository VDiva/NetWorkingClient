using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h=Input.GetAxis("Horizontal");
        var v=Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h,0,v)*Time.deltaTime*5,Space.World);
    }
}
