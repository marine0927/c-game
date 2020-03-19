using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour {

    #region 属性
    public Vector2 speed = new Vector2(10, 10);

    public Vector2 direction = new Vector2(1, 0);

    private Vector2 movement;

    public int isEnemy = 0;

    public bool isEnemyShot = false;

    float Lifetime = 1.4f;

    #endregion
    // Use this for initialization
    void Start () {
        InvokeRepeating("Time_count", 0.0f, 0.1F);
        direction.x = GameObject.Find("Hero").GetComponent<PlayerControl>().xDirection;
        direction.y = GameObject.Find("Hero").GetComponent<PlayerControl>().yDirection;
        if (direction.x == 1 && direction.y == 0) transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        if (direction.x == 1 && direction.y == 1) transform.rotation = Quaternion.Euler(0f, 0f, -135f);
        if (direction.x == -1 && direction.y == 0) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (direction.x == 1 && direction.y == -1) transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        if (direction.x == -1 && direction.y == 1) transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        if (direction.x == -1 && direction.y == -1) transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        if (GameObject.Find("Hero").GetComponent<Rigidbody2D>().velocity.x == 0 && direction.y == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f); direction.x = 0;
        }
        transform.position = transform.position+new Vector3(direction.x,direction.y,0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Time_count()
    {
        if (Lifetime > 0)
        {
            Lifetime-=0.1f;
        }
        else
        {
            CancelInvoke();
            Destroy(transform.gameObject);
        }
    }
}
