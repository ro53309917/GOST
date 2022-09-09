using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && collision.gameObject.name == "T_1 1(Clone)")
        {
            Debug.Log("ËÀÍö×´Ì¬´¥·¢");
        }
    }
}
