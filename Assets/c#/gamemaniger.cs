using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class gamemaniger : MonoBehaviour
{
    public GameObject player;
    public GameObject star;
    public GameObject end;
    public bool test = false;
    public Rigidbody2D aaaa;//应该没用
    public GameObject Cinemachine;
   
    

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position=star.transform.position;
        end=GameObject.Find("end");
        star = GameObject.Find("star");
        player= Instantiate(player);
        Cinemachine.GetComponent<CinemachineVirtualCamera>().Follow=player.transform;//初始化位置


    ;
    }

    private void GetCinemachineComponent<T>()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameObject.Find("player(Clone)").GetComponent<Player>().endstar==true)
        //{
        //    aaa = true;
        //    Debug.Log("Hello");
        //    GameObject.Find("player").GetComponent<Player>().endstar = false;


        //}
        if(player.GetComponent<Player>().reversal)
        {
            zhongdianduihuan(star, end);
            player.GetComponent<Player>().reversal = false;
        }

    }
    void zhongdianduihuan(GameObject a,GameObject b)//起点终点位置转换
    {
        Vector3 temp;
        temp = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = temp;
    }
}
