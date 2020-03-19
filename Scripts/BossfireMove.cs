using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossfireMove : MonoBehaviour {


    
    // Use this for initialization
    public Vector2 direction;

    public Vector2 speed = new Vector2(10, 10);

    public int isEnemy = 0;

    int Lifetime = 2;


    void Start () {
        InvokeRepeating("Time_count", 0.0f, 1.0F);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        transform.GetComponent<Rigidbody2D>().velocity = movement;
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
        PlayerControl hero = otherCollider.gameObject.GetComponent<PlayerControl>();
        if (hero!= null)
        {
            // 判断子弹归属,避免误伤
            if (hero.isEnemy==-1)
            {
                GameObject.Find("Hero").GetComponent<PlayerControl>().Hp -= 10;
            }
        }
    }
}
