using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {


    public static bool zombieMode = true;
    public float timer = 10;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

        if (!zombieMode) {


            ManaScript.manaValue -= (20 / timer) * Time.deltaTime;
            if (ManaScript.manaValue <= 0) {
                zombieMode = true;

            }
                

                }

	}
}
