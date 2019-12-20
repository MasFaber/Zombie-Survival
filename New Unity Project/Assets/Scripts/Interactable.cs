using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 0.3f;

    public GameObject player;
    bool wasInRange = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < radius)
        {
            Debug.Log("Is in Range");

            //set interactable in player controller
            player.GetComponent<PlayerController>().SetClosestInteractable(this);
            wasInRange = true;
        }
        if (distance >= radius*2 && wasInRange==true) 
        {
            player.GetComponent<PlayerController>().SetClosestInteractable(null);
            wasInRange = false;
        }
    }

    public virtual void Interact()
    {
        //this method is meant to be overwritten
        Debug.Log("interacting");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.DrawWireSphere(transform.position, radius*2);
    }
}
