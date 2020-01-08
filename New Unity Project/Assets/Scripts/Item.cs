
using UnityEngine;


[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    
    public int rank = 1;
    public int value = 500;

    public int attackDamageModifier;
    public int armorModifier;
    public int attackSpeedModifier;
    public int lifeStealModifier;
    public int armorPenetrationModifier;
    public int critChanceModifier;

}
