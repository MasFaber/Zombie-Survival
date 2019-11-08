using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaScript: MonoBehaviour
{

    public static float manaValue = 0;
    public float maxMana = 30;
    Text mana;

    // Use this for initialization
    void Start()
    {
        mana = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        mana.text = "Mana: " + System.Math.Round(manaValue,0);

        if (ManaScript.manaValue > maxMana)
        {
            ManaScript.manaValue = maxMana;
        }
    }
}
