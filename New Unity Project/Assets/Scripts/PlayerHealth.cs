using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth=100;

    public static int playerHealth;

    public GameObject player;


    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        playerHealth = maxPlayerHealth;
    }

     void Update()
    {
        
        //deathcheck
        if (playerHealth <= 0) {
            playerHealth = 0;
            Destroy(player);
        }

        text.text = "Health " + playerHealth;
    }

    public static void Hurtplayer(int damageToGive) {
        playerHealth -= damageToGive;

    }

    public void FullHealth() {
        playerHealth = maxPlayerHealth;
    }
}