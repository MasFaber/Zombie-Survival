using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public float maxZombieHealth = 100;

    public float zombieHealth;

    public GameObject zombie;


    public Image healthBar;

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
        }

    }

    public void Hurtenemy(int damageToGive)
    {
        zombieHealth -= damageToGive;
        healthBar.fillAmount = zombieHealth/maxZombieHealth;
        
    }

    public void FullHealth()
    {
        zombieHealth = maxZombieHealth;
    }
}