using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEMANGER : MonoBehaviour
{
    public GameObject player_f;
    public bool star=false;
    public GameObject player;
    public Rigidbody player_r;
    public float speed=1;
    public bool Timea = false;
    public int Time_length=10;
    public float Time_grow = 1f;
    public int Time_temp = 0;
    public float test =0;
    public List<GameObject> Instantiation;
    public int difficulty = 1;
    public int difficulty_time = 300;
    public GameObject trap;
    public int time_loop = 300;
    public int time_loop_top = 0;
    public bool test_2;//���Կ���
    public float down_speed=5;//׹���ٶ�
    public GameObject Instantiate_move;
    public float Instantiate_move_z;
    public List<GameObject> trap_group;
    public bool trap_group_visual = true;//������ʾ��ʱ��״̬�л�
    public float stop_time=1;

    // Start is called before the first frame update
   
    void Start()
    {
        player_r = player.GetComponent<Rigidbody>();
        Instantiate_move_z = Instantiate_move.transform.position.z;

    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
    // Update is called once per frame

    void Update()
    {
        if (star)
        {
            test = Time.timeScale;
            Move(player_r,ref player_f);
            Time_stop(ref Timea);
            Time_loop();
            Instant_Move(Instantiate_move_z);
            stop();
            if (Time.timeScale < 1)
            {
                Time.timeScale = Time.timeScale + 0.01f;
            }
        }


    }

    public void Move(Rigidbody a,ref GameObject b)
    {
        float y=0;
        float x=0;
        if(player.transform.position.y>-1&& player.transform.position.y<1)//λ�����ƹ���
        {
            y = Input.GetAxisRaw("Vertical") * speed;
        }
        else if(player.transform.position.y > -1)
        {
            y = -1;
        }
        else if (player.transform.position.y <1)
        {
            y = 1;
        }
            //�����Ҹ�ʱ�任�ɽ���
            if (player.transform.position.x > -1 && player.transform.position.x < 1)//λ�����ƹ���
        {
             x = Input.GetAxisRaw("Horizontal") * -speed;
        }
        else if (player.transform.position.x > -1)
        {
            x = -1;
        }
        else if (player.transform.position.x < 1)
        {
            x = 1;
        }
        //a.velocity = new Vector3(x, y, a.velocity.z);
        float z = down_speed;
        a.velocity = new Vector3(x, y, z);//����λ��
        b.GetComponent<Rigidbody>().velocity = new Vector3(b.GetComponent<Rigidbody>().velocity.x, b.GetComponent<Rigidbody>().velocity.y, down_speed);//����λ��
    }
    public void Time_stop(ref bool a)
    {
        if(a&&Time.timeScale>0)
        {
            Debug.Log("������13");
            Time_grow = Time_grow - 0.01f;
            if(Time_grow<0)
            {
                Time_grow = 0;
            }
            Time.timeScale = Time_grow;
   
        }
        else if(a&&Time.timeScale<=0)
        {
           
            Time_length = Time_length - 1;

            if (Time_length <= 0)
            {
                Debug.Log("������1");
                a = false;
                Time_length = 90;

            }
        }
        else if(a!&&Time.timeScale<1)
        {
            Debug.Log("������");
            Time_grow = Time_grow + 0.01f;
            if (Time_grow > 1)
            {
                Time_grow = 1;
            }
            Time.timeScale = Time_grow;//��־��
        }

    }
    public void Time_add_subtract  (ref bool a)
    {

    }
    public void Time_stop_2()
    {
        Time_length = Time_length - 1;
    }
    public void instantiatio (ref List<GameObject> a)
    {
        int aaa = Random.Range(0,9);//���������
        var bbb = a[aaa];
        GameObject c= Instantiate(trap,bbb.transform.position,new Quaternion(0,0,0,0));//����һ��
        //if(trap_group_visual!)
        //{
        //    Debug.Log("1");
        //    c.GetComponent<MeshRenderer>().enabled = false;
        //}//ûʲô��(�����������ɵ�ʱ�򷽿ɼ�״̬)

        trap_group.Add(c);
    }
    public void Time_loop()
    {
        time_loop_top++;
        if(time_loop_top>time_loop)
        {
            if(trap_group_visual)
            {
                instantiatio(ref Instantiation);
            }

            time_loop_top = 0;
        }
      
    }

    public void Instant_Move(float a)//��������λ��
    {
        Instantiate_move.transform.position = new Vector3(Instantiate_move.transform.position.x, Instantiate_move.transform.position.y ,a+ player.transform.position.z);
    }


    public void stop()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trap_group_visual = false;
            foreach (var item in trap_group)
            {
                item.GetComponent<MeshRenderer>().enabled=false;//���ڿ�������������
            }
            

        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            trap_group_visual = true;
            foreach (var item in trap_group)
            {
                item.GetComponent<MeshRenderer>().enabled =true;//���ڿ�������������
            }
            stop_time = 1;







        }
        if(Input.GetKey(KeyCode.Space))
        {

            stop_time = stop_time - 0.01f;
            Time.timeScale = stop_time;
            //Time.timeScale = 0;
            if (stop_time<0.02f)
            {
                stop_time = 0.02f;
            }
        }
    }
    public void RayLine()
    {
       // RaycastHit hit=Physics.BoxCast()
    }
}
