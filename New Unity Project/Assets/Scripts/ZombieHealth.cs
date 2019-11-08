using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public float maxZombieHealth = 100;

    public int zombieValue = 10;

    public float zombieHealth;

    public GameObject zombie;


    public Image healthBar;

    //for the dropables
    public GameObject mana;


    void Start()
    {
        zombieHealth = maxZombieHealth;
    }

    void Update()
    {

        //deathcheck
        if (zombieHealth <= 0)
        {
            zombieHealth = 0;
            Destroy(zombie);
            //add points
            ScoreScript.scoreValue += zombieValue;

            Instantiate(mana, transform.position, Quaternion.identity);

        }

    }

    public void Hurtenemy(int damageToGive)
    {
        if (StateMachine.zombieMode)
        {
            zombieHealth -= damageToGive;
            healthBar.fillAmount = zombieHealth / maxZombieHealth;
            ScoreScript.scoreValue += 10;
        }
    }

    public void FullHealth()
    {
        zombieHealth = maxZombieHealth;
    }
}