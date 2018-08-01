using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SlimeController : MonoBehaviour {


    //old
    public float MoveSpeed;

    private Rigidbody2D MyRigidBody;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    //new

    public float lookRadius=15f;
    private Transform target1;
    private Transform target2;
    private Transform target;

    private float distance;

    //attacking
    public float attackRadius = 3f;
    public float attackSpeedMultiplier = 0.5f;
    public float attackTime = 3f;
    private float attackTimer = 0f;
    public float attackCooldown = 4f;
    private float attackCooldownTimer = 0f;
    private bool attacking = false;
    private Vector3 location=Vector3.zero;

    // private Animator anim;


    // damage

    public int damageToGive = 5;


    // Use this for initialization

    void Start ()
    {
        
        MyRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        //make sure to keep attacking with same target
       // anim.SetBool("Attacking", attacking);
        if (attackTimer > 0)
        {
            Attack();
            return;
        }
        else
        {
            attacking = false;
        }


        //find distance to champ
            

        if (Playermanager.instance.player1 == null || Playermanager.instance.player2 == null) {
            if (Playermanager.instance.player1 == null) {
                target = Playermanager.instance.player2.transform; ;
                distance = Vector2.Distance(target2.position, transform.position);
            }
            if (Playermanager.instance.player2 == null)
            {
                target = Playermanager.instance.player1.transform; ;
                distance = Vector2.Distance(target1.position, transform.position);
            }
        }
        else
        {
            target1 = Playermanager.instance.player1.transform;
            target2 = Playermanager.instance.player2.transform;
            float distance1 = Vector2.Distance(target1.position, transform.position);
            float distance2 = Vector2.Distance(target2.position, transform.position);
            if (distance1 < distance2)
            {
                target = target1;
                distance = distance1;
            }

            else
            {
                target = target2;
                distance = distance2;
            }
        }

        if (distance <= lookRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            
        }

        if (distance <= attackRadius && attackCooldownTimer<0)
        {
            location = target.position;
            attackCooldownTimer = attackCooldown;
            attackTimer = attackTime;
            Attack();

        }


        if (moving) {
            timeToMoveCounter -= Time.deltaTime;
            MyRigidBody.velocity = moveDirection;
            if (timeToMoveCounter < 0)
            {
                moving = false;
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

            }

        }
        else {
            timeBetweenMoveCounter -= Time.deltaTime;
            MyRigidBody.velocity=Vector2.zero;


            if(timeBetweenMoveCounter < 0f) {
                moving = true;
                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f, 1f)*MoveSpeed, Random.Range(-1f, 1f)*MoveSpeed, 0f);


            }
        }

        attackCooldownTimer -= Time.deltaTime;

    }

    public void Attack() {
        //move toward target at double speed
        Debug.Log("Attacking");
        transform.position = Vector2.MoveTowards(transform.position, location, MoveSpeed*attackSpeedMultiplier * Time.deltaTime);
        attackTimer -= Time.deltaTime;
        return;
        //animation
    }


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            //how to reference the correct Playerhealth script here?
            //deal damage
            PlayerHealth.Hurtplayer(damageToGive);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

