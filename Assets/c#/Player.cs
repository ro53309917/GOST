using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool reversal = false;//����λ��
    public bool speeddoublu = false;
    public bool endstar = false;
    public bool chongcibool = true;
    //ʱ�䵥λͳһΪ60֡ÿ���µ�֡
    public bool double_jump = true;
    public float direction_control = 0;//�������� ����Ϊ1 ��Ҫ�����ڼ��������ӵ����ֵ
    public GameObject gruond_jiance;
    public bool face=true;
    public Rigidbody2D player;
    public bool jump=true;//�����Ծ����
    public float jumpfroce = 0;
    public bool ground;//��Ҵ��ڵ��沼��
    public float speed=10;//����ٶȱ���
    //public float speed_hit_time = 0;//�������� ����Ϊ1 ��Ҫ�����ڼ��������ӵ����ֵ
    public float speed_hit_star_add = 0.01f;//���趨�ļ���ֵ
    public float speed_hit_end_reduce = 0.01f;//���趨�ļ���ֵ
    public float speed_hit_star_time = 3;//��������ٵ������ٶ�,����Ϊ60֡ÿ����Ϊ3֡,0.05��
    public float speed_hit_end_time = 6;//���������Ϊ0,����Ϊ60֡ÿ����Ϊ6֡,0.1��
    public bool yucaozuobool = false;
    public int yucaozuotime = 0;
    public int yucaozuotimemax = 100;
    public bool chongcikaiguan = false;
    public int chongcitime = 0;
    public bool pengzhuangtest=false;
    // Start is called before
    // the first frame update
    void speed2bei()
    {
        if(speeddoublu!)
        {
            speed = speed * 2;
        }
        else if (speeddoublu)
        {
            speed = speed / 2;
        }

    }
    void singleRay2D   ()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, new Vector2(0, -1), 1, LayerMask.GetMask("Ground"));

        if(hit.collider!=null)
        {
            pengzhuangtest = true;
            Debug.DrawLine(transform.position, hit.point);
            double_jump = true;
            
        }
        else if(hit.collider == null)
        {
            pengzhuangtest = false;
            Debug.DrawLine(transform.position, transform.position+ new Vector3(0, -1,0));
        }

        

    }

    void Start()
    {
        speed_hit_star_add = 1 / speed_hit_star_time;
        speed_hit_end_reduce = 1 / speed_hit_end_time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)&&chongcibool)
        {
            chongcikaiguan = true;
        }
        chongci();
        face_contion();
        Move();
        Double_jump();
        
        Jump();
        yucaozuo();
        face_jump();
        singleRay2D();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{
        //    jump = true;//��غ���ԾΪ��
        //    double_jump = true;//��غ������Ϊ��
        //    if (yucaozuobool)
        //    {
        //        player.AddForce(new Vector2(0, jumpfroce), ForceMode2D.Impulse);
        //        jump = false;
        //    }//Ԥ�Ȳ�������

        //}
        if (collision.gameObject.layer == 7&&collision.gameObject.name=="T_1")
        {
            reversal = true;
        }
        if (collision.gameObject.layer == 7 && collision.gameObject.name == "T_2")
        {
            speeddoublu = true;
            speed2bei();
        }
        if (collision.gameObject.layer == 7 && collision.gameObject.name == "T_3")
        {
            
            endstar = true;
        }
        if (collision.gameObject.layer == 7 && collision.gameObject.name == "star")
        {

            endstar = true;
        }
        if (collision.gameObject.layer == 7 && collision.gameObject.name == "end")
        {

            endstar = true;
        }

    }//��Ծˢ�¼�� �������
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{
        //    if(jump)
        //    {
        //        jump = false;
        //    }
            
        //}//����˲�� ��ԾΪ��

    }

    //public void Rspeed()
    //{
    //    if(Input.GetAxisRaw("Horizontal")!=0)//�����߼�
    //    {
    //        speed_hit_time = speed_hit_time + speed_hit_star_add;
    //    }
    //    else
    //    {
    //        speed_hit_time = speed_hit_time - speed_hit_end_reduce;
    //    }
    //    if(speed_hit_time>1)
    //    {
    //        speed_hit_time = 1;
    //    }
    //    if(speed_hit_time<0)
    //    {
    //        speed_hit_time = 0;
    //    }

    //}//���� ����ʱ��
    public void Move()
    {
        if(chongcikaiguan==false)
        { 
            player.velocity = new Vector3(direction_control * speed, player.velocity.y, 0);
        }
        //player.velocity = new Vector3(direction_control * speed, player.velocity.y, 0);
        //if (Input.GetAxisRaw("Horizontal") != 0) //���λ�Ʋ�����0
        //{ 
        //    player.velocity = new Vector3(speed_hit_time * Input.GetAxisRaw("Horizontal") * speed, player.velocity.y, 0);//��ô��ǰλ�Ƴ˼��ٷ�����ٶ�
        //}
        //else if(face)
        //{
        //    player.velocity = new Vector3(speed_hit_time *1* speed, player.velocity.y, 0);//���Ϊ��������Ϊ0�� �жϵ�ǰ��������������
        //}
        //else if (face==false)
        //{
        //    player.velocity = new Vector3(speed_hit_time *-1 * speed, player.velocity.y, 0);
        //}

    }//λ���߼�
    public void Double_jump()
    {
        if (double_jump && (Input.GetKeyDown(KeyCode.Space)) && pengzhuangtest == false)
        {
            player.velocity = new Vector3(player.velocity.x, 0);
            player.AddForce(new Vector2(0, jumpfroce), ForceMode2D.Impulse);
            double_jump = false;
        }
    }

    public void Jump_move()
    {
        player.velocity = new Vector3(direction_control * speed, player.velocity.y, 0);
    }

    public void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true) && pengzhuangtest)
        {

            player.AddForce(new Vector2(0, jumpfroce), ForceMode2D.Impulse);
        }

    }//��Ծ�߼�

    public void face_contion()
    {
        if (chongcikaiguan == false)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                face = true;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                face = false;
            }
        }

    }//�泯�������

    public void face_jump()//���ټ������ҿ���
    {

        if(true)
        {
            if(reversal==false)
            {
                            if(Input.GetAxisRaw("Horizontal") ==1)
                                {
                                         direction_control = direction_control + speed_hit_star_add;
                                 }
                            if (Input.GetAxisRaw("Horizontal") == -1)
                                {
                                         direction_control= direction_control - speed_hit_star_add;
                                 }
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    direction_control = direction_control - speed_hit_star_add;
                }
                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    direction_control = direction_control + speed_hit_star_add;
                }
            }

            if(Input.GetAxisRaw("Horizontal") == 0)
            {
                if(face)
                {
                    direction_control = direction_control - speed_hit_end_reduce;
                    if(direction_control<0)
                    {
                        direction_control = 0;
                    }

                }
                else if(face==false)
                {
                    direction_control = direction_control + speed_hit_end_reduce;
                    if (direction_control > 0)
                    {
                        direction_control = 0;
                    }
                }

            }
        }
        if(direction_control>1)
        {
            direction_control = 1;
        }
        if(direction_control<-1)
        {
            direction_control = -1;
        }
        //if (Input.GetAxisRaw("Horizontal") == 0)
        //{
        //    if(direction_control<0.05&&direction_control>-0.05)
        //    {
        //        direction_control = 0;
        //    }

        //}


    }//û�����������


    public void yucaozuo()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&jump==false)
        {
            yucaozuobool = true;
        }


        
        if(yucaozuobool)
        {
            yucaozuotime++;
            if(yucaozuotime>yucaozuotimemax)
            {
                yucaozuobool = false;
                yucaozuotime = 0;
            }
        }

    }

    public void chongci()
    {
        if(chongcikaiguan)
        {
            chongcibool = false;
            chongcitime++;
            if(chongcitime>40)
            {
                chongcitime = 0;
                chongcikaiguan = false;
            }
            if(face)
            {
                player.velocity = new Vector2(20, 0);
            }
            else if(face==false)
            {
                player.velocity = new Vector2(-20, 0);
            }
            //switch(face)
            //{
                
            //    case true:
            //        player.velocity = new Vector2(10,0);
            //        break;
            //    case false:
            //        player.velocity = new Vector2(-10,0);
            //        break;
            //}
                
        }
    }
    public void chufa()//��������
    {
        
    }
    public void speeddoubl()
    {

    }

}
