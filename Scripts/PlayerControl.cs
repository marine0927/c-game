using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    enum SkillKey{Skill_J=0,Skill_U=1,Skill_L=2, Skill_I=3, Skill_O=4 };
    public GameObject shot_tama;
    public GameObject shot_arrow;
    public GameObject shot_bow;
    public Animator Player_Walk;//运动动画
    public Rigidbody2D Player_Regidbody;//角色刚体
    public float Player_Speed;
    public float Player_JumpHeight;
    public bool If_Jump;
    public int JumpTimes;
    public float Gravity;
    public int xDirection = 1;
    public int yDirection = 0;
    bool []onCDtime = new bool[] { true,true,true,true,true };
    float[] CDtime = new float[] { 1.5F, 2F, 0.5F, 0.5F, 0.5F };
    float []CDnow=new float[5];
    public int Hp=100;
    public int initHp=100;
    public int isEnemy = -1;
    // Use this for initialization
    private void Awake()
    {
        Player_Regidbody = GetComponent<Rigidbody2D>();
        Player_Speed = 4f;
        Player_JumpHeight = 350f;
        If_Jump = false;
        Gravity = 2f;
        JumpTimes = 0;
        Hp = 100;
        initHp = 100;
        xDirection = 1;
        yDirection = 0;
}
    void Start () {
        InvokeRepeating("Time_count", 0.0f, 0.1f);
    }

    // Update is called once per frame
    void Update () {

        Player_Regidbody.AddForce(Vector3.down * Gravity);//增加重力

        #region 角色移动控制
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.rotation == Quaternion.Euler(0f, 0f, 0f) && Player_Walk.GetBool("StandToWalk") == true)
            {
                Player_Regidbody.velocity = new Vector2(0, Player_Regidbody.velocity.y) + new Vector2(1, 0)* Player_Speed;
                
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                xDirection = 1;
                Player_Walk.SetBool("StandToWalk", true);
            }
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            Player_Regidbody.velocity = new Vector2(0, Player_Regidbody.velocity.y) + new Vector2(0, 0);
            Player_Walk.SetBool("StandToWalk", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.rotation == Quaternion.Euler(0f, 180f, 0f) && Player_Walk.GetBool("StandToWalk") == true)
            {
                Player_Regidbody.velocity = new Vector2(0, Player_Regidbody.velocity.y) + new Vector2(-1, 0)* Player_Speed;

            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                xDirection = -1;
                Player_Walk.SetBool("StandToWalk", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            Player_Regidbody.velocity = new Vector2(0, Player_Regidbody.velocity.y) + new Vector2(0, 0);
            Player_Walk.SetBool("StandToWalk", false);
            
        }
        #endregion

        #region 角色跳跃
        if (Input.GetKey(KeyCode.K))
        {
            if (!If_Jump && JumpTimes <= 1)
            {
                JumpTimes++;
                Player_Regidbody.AddForce(Vector2.up * Player_JumpHeight);
                If_Jump = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            If_Jump =false;           
        }


        #endregion


        if (onCDtime[(int)SkillKey.Skill_J]&&Input.GetKey(KeyCode.J))
        {
            onCDtime[(int)SkillKey.Skill_J] = false;
            CDnow[(int)SkillKey.Skill_J] = CDtime[(int)SkillKey.Skill_J];
            // 指定子弹位置
            GameObject shottama = Instantiate(shot_tama,
                                transform.position,
                                transform.rotation)
                                as GameObject;


            // 设置子弹归属

        }

        if (onCDtime[(int)SkillKey.Skill_U] && Input.GetKey(KeyCode.U))
        {

            onCDtime[(int)SkillKey.Skill_U] = false;
            CDnow[(int)SkillKey.Skill_U] = CDtime[(int)SkillKey.Skill_U];        
            // 指定子弹位置
            GameObject arrow = Instantiate(shot_arrow,
                                transform.position,
                                transform.rotation)
                                as GameObject;
            GameObject bow = Instantiate(shot_bow,
                    transform.position,
                    transform.rotation)
                    as GameObject;


            // 设置子弹归属

        }

        if (onCDtime[(int)SkillKey.Skill_L] && Input.GetKey(KeyCode.L))
        {

            onCDtime[(int)SkillKey.Skill_L] = false;
            CDnow[(int)SkillKey.Skill_L] = CDtime[(int)SkillKey.Skill_L];
            Player_Speed = 25;

        }

        if (Input.GetKey(KeyCode.W))
        {
            yDirection = 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            yDirection = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yDirection = -1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            yDirection = 0;
        }
        GameObject.Find("herobloodbar").GetComponent<Image>().fillAmount = (float)Hp/(float)initHp;
        if(Hp<=0)
        {
        }
    }
    void Time_count()
    {
        for (int i = 0; i < 5; i++)
        {
            if (CDnow[i] > 0)
            {
                if (i == 2)
                {
                    Player_Speed = 4;
                }
                CDnow[i] -= 0.1f;
            }
            else
            {
                onCDtime[i] = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BackGround backGround = collision.gameObject.GetComponent<BackGround>();
        if (backGround != null)
        {
            if (backGround.gameObject.transform.position.y < transform.position.y)
            {
                // 判断子弹归属,避免误伤
                if (backGround.isGround)
                {
                    JumpTimes = 0;
                }
            }
        }
    }


}
