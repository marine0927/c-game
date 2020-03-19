using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBlood : MonoBehaviour {


    // Use this for initialization
    void Start () {
        InvokeRepeating("Time_count", 0.0f, 1.0F);

        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void Time_count()
    {
        print(1);
        GetComponent<Image>().fillAmount -= 0.1f;
    }
}
