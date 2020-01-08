using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null) {

            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
            instance = this;
       
    }

    #endregion

    public delegate void OnItemChanged(Item newItem, Item oldItem);
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space=6;

    public bool Add(Item item) {

        if (items.Count >= space)
        {
            Replace(item,items[0]);
            return true; // now this is also true
        }
        items.Add(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(item,null);
        }


        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(null,item);
        }
    }

    public void Replace(Item newItem, Item oldItem)
    {
        items.Remove(oldItem);
        items.Add(newItem);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(null, oldItem);
            onItemChangedCallback.Invoke(newItem, null);
        }
    }
}
