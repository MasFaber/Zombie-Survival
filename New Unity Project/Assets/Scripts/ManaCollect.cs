using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollect : MonoBehaviour {

    public float manaValue = 10;
    public float manaStartTimer = 10;
    private float manaTimer;



	// Use this for initialization
	void Start () {
        manaTimer = manaStartTimer;
		
	}
	
	// Update is called once per frame
	void Update () {
        manaTimer -= Time.deltaTime;

        if (manaTimer < 0) {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
                ManaScript.manaValue +=manaValue;

         
                // ref to soundmanager script.
                Destroy(gameObject);

        }
    }
}