﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public KeyCode attackButton1 = KeyCode.G;
    public int damageToGive;
    public int damAtt1 = 10;
    //public KeyCode attackButton2 = KeyCode.H;
    private PlayerController controller;
    private Vector2 direction;
    private float hitDirection;
    private Transform playerTransform;

    public Collider2D[] attackHitboxes;

    public GameObject Attack;
    public GameObject BasicAttackPrefab;
    // Use this for initialization
    void Start() {
        controller = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = controller.lastDirection;

        if (Input.GetKeyDown(attackButton1))
        {
            BasicAttack();

        }
        //if (Input.GetKeyDown(attackButton2))
        //{
        //    LaunchAttack(attackHitboxes[1]);
        //} 
    }

    private void BasicAttack()
    {
        Debug.Log("Attack");
    
        hitDirection = XYToDegrees(direction);

        attackHitboxes[0].transform.eulerAngles = new Vector3(attackHitboxes[0].transform.eulerAngles.x, 
        attackHitboxes[0].transform.eulerAngles.y, hitDirection);


        //GameObject clone;

        //Instantiate(Attack, transform.position, attackHitboxes[0].transform.rotation);

        BasicAttackPrefab.transform.rotation = attackHitboxes[0].transform.rotation;

        attackHitboxes[0] = BasicAttackPrefab.GetComponent<BoxCollider2D>();
            //set damage properly;
        damageToGive = damAtt1;
           
        LaunchAttack(attackHitboxes[0]);
}
    private void LaunchAttack(Collider2D col)
    {
        //sphere might be better.
        Collider2D []cols = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size,col.transform.eulerAngles.z, LayerMask.GetMask("Enemy"));
        //Collider[] cols= Physics.OverlapBox()
        foreach (Collider2D collider in cols)
        {
            if (collider.tag == "Enemy") //not sure if needed
            {
                GameObject Enemy = collider.gameObject;
                //GameObject bodyParent = body.transform.parent.gameObject;
                Enemy.GetComponent<ZombieHealth>().Hurtenemy(damageToGive);

                //knockback
                playerTransform = GetComponent<Transform>();
                Vector3 moveDirection = Enemy.transform.position - playerTransform.transform.position;
             

                Enemy.GetComponent<Rigidbody2D>().AddForce(moveDirection.normalized*100f);
                Enemy.GetComponent<SlimeController>().knockbackTimer =0.5f; //this 1f need to be set per attack
            }
           
        }

    }


        private float XYToDegrees(Vector2 xy)
    {
        double radAngle = Math.Atan2(xy.x, xy.y);
        double degreeAngle = radAngle * 180.0 / Math.PI;
        hitDirection = (float)(90-degreeAngle);
        return hitDirection;

    }
}
