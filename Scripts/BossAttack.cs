using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttack : MonoBehaviour {

    // Use this for initialization

    public GameObject boss_fire;
    public int Hp = 100;
    public int initHp = 100;
    public int isEnemy = 1;
    void Start () {
        InvokeRepeating("Time_count", 0.0f, 1.0F);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("bossbloodbar").GetComponent<Image>().fillAmount = (float)Hp / (float)initHp;
        if (Hp <= 0)
        {
        }
    }

    void Time_count()
    {
        GameObject bossfire = Instantiate(boss_fire,
                                 transform.position,
                                 transform.rotation)
                                 as GameObject;
        bossfire.GetComponent<BossfireMove>().direction = new Vector2(-1, 0);
    }
}
