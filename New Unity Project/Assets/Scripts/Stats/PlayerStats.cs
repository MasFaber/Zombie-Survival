using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats: MonoBehaviour
{
    public int maxPlayerHealth=100;

    public static int playerHealth;

    public GameObject player;

    public Stat attackDamage;
    public Stat armor;
    public Stat attackSpeed;
    public Stat lifeSteal;
    public Stat armorPenetration;
    public Stat critChance;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        playerHealth = maxPlayerHealth;
        Inventory.instance.onItemChangedCallback += OnItemChanged;
    }

    // Called when an item gets equipped/unequipped
    void OnItemChanged(Item newItem, Item oldItem)
    {
        // Add new modifiers
        if (newItem != null) //if there is a new item
        {
            armor.AddModifier(newItem.armorModifier);
            attackDamage.AddModifier(newItem.attackDamageModifier);
            attackSpeed.AddModifier(newItem.attackSpeedModifier);
            lifeSteal.AddModifier(newItem.lifeStealModifier);
            armorPenetration.AddModifier(newItem.armorPenetrationModifier);
            critChance.AddModifier(newItem.critChanceModifier);
        }

        // Remove old modifiers
        if (oldItem != null) //  if there was an old item
        {
            armor.RemoveModifier(oldItem.armorModifier);
            attackDamage.RemoveModifier(oldItem.attackDamageModifier);
            attackSpeed.RemoveModifier(oldItem.attackSpeedModifier);
            lifeSteal.RemoveModifier(oldItem.lifeStealModifier);
            armorPenetration.RemoveModifier(oldItem.armorPenetrationModifier);
            critChance.RemoveModifier(oldItem.critChanceModifier);
        }

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