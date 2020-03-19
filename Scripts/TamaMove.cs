using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaMove : MonoBehaviour
{
    #region 属性
    public Vector2 speed = new Vector2(10, 10);

    public Vector2 direction = new Vector2(1, 0);

    private Vector2 movement;

    public int isEnemy = 0;

    public bool isEnemyShot = false;

    int Lifetime = 2;

    #endregion
    void Start()
    {
        InvokeRepeating("Time_count", 0.0f, 1.0F);
        direction.x= GameObject.Find("Hero").GetComponent<PlayerControl>().xDirection;
        direction.y= GameObject.Find("Hero").GetComponent<PlayerControl>().yDirection;
        if (direction.x == 1 && direction.y == 1)transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        if (direction.x == -1 && direction.y == 0)transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        if (direction.x == 1 && direction.y == -1) transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        if (direction.x == -1 && direction.y == 1) transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        if (direction.x == -1 && direction.y == -1) transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        if (GameObject.Find("Hero").GetComponent<Rigidbody2D>().velocity.x == 0 && direction.y == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f); direction.x = 0;
        }
            
    }

    void Update()
    {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        transform.GetComponent<Rigidbody2D>().velocity = movement;
    }

    void FixedUpdate()
    {

    }

    void Time_count()
    {
            if (Lifetime > 0)
            {
                Lifetime--;
            }
            else
            {
                CancelInvoke();
                Destroy(transform.gameObject);
            }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        BossAttack boss = otherCollider.gameObject.GetComponent<BossAttack>();
        if (boss != null)
        {
            // 判断子弹归属,避免误伤
            if (boss.isEnemy == 1)
            {
                boss.Hp -= 5;
            }
        }
    }
}
