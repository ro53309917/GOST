using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool reversal = false;//调换位置
    public bool speeddoublu = false;
    public bool endstar = false;
    public bool chongcibool = true;
    //时间单位统一为60帧每秒下的帧
    public bool double_jump = true;
    public float direction_control = 0;//加速上限 上限为1 需要设置在几秒内增加到这个值
    public GameObject gruond_jiance;
    public bool face=true;
    public Rigidbody2D player;
    public bool jump=true;//玩家跳跃布尔
    public float jumpfroce = 0;
    public bool ground;//玩家处于地面布尔
    public float speed=10;//玩家速度变量
    //public float speed_hit_time = 0;//加速上限 上限为1 需要设置在几秒内增加到这个值
    public float speed_hit_star_add = 0.01f;//被设定的加速值
    public float speed_hit_end_reduce = 0.01f;//被设定的减速值
    public float speed_hit_star_time = 3;//多少秒加速到极限速度,主角为60帧每秒中为3帧,0.05秒
    public float speed_hit_end_time = 6;//多少秒减速为0,主角为60帧每秒中为6帧,0.1秒
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
        //    jump = true;//落地后跳跃为真
        //    double_jump = true;//落地后二段跳为真
        //    if (yucaozuobool)
        //    {
        //        player.AddForce(new Vector2(0, jumpfroce), ForceMode2D.Impulse);
        //        jump = false;
        //    }//预先操作部分

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

    }//跳跃刷新检测 触碰检测
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{
        //    if(jump)
        //    {
        //        jump = false;
        //    }
            
        //}//起跳瞬间 跳跃为假

    }

    //public void Rspeed()
    //{
    //    if(Input.GetAxisRaw("Horizontal")!=0)//减速逻辑
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

    //}//加速 减速时间
    public void Move()
    {
        if(chongcikaiguan==false)
        { 
            player.velocity = new Vector3(direction_control * speed, player.velocity.y, 0);
        }
        //player.velocity = new Vector3(direction_control * speed, player.velocity.y, 0);
        //if (Input.GetAxisRaw("Horizontal") != 0) //如果位移不等于0
        //{ 
        //    player.velocity = new Vector3(speed_hit_time * Input.GetAxisRaw("Horizontal") * speed, player.velocity.y, 0);//那么当前位移乘加速方向乘速度
        //}
        //else if(face)
        //{
        //    player.velocity = new Vector3(speed_hit_time *1* speed, player.velocity.y, 0);//如果为方向向量为0了 判断当前方向慢慢慢下来
        //}
        //else if (face==false)
        //{
        //    player.velocity = new Vector3(speed_hit_time *-1 * speed, player.velocity.y, 0);
        //}

    }//位移逻辑
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

    }//跳跃逻辑

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

    }//面朝方向控制

    public void face_jump()//加速极限左右控制
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


    }//没做完的面向方向


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
    public void chufa()//触发开关
    {
        
    }
    public void speeddoubl()
    {

    }

}
