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

    public float lookRadius=10f;
    private Transform target1;
    private Transform target2;
    private Transform target;

    private float distance;



    // Use this for initialization

    void Start () {
        target1 = Playermanager.instance.player1.transform;
        target2 = Playermanager.instance.player2.transform;
        //agent = GetComponent<NavMeshAgent>();
        MyRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance1 = Vector2.Distance(target1.position, transform.position);
        float distance2 = Vector2.Distance(target2.position, transform.position);

        if (distance1 < distance2)
        {
            target = target1;
            distance = distance1;
        }
        else {
            target = target2;
            distance = distance2;
        }

        if (distance <= lookRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            
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
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player") {
            Destroy(other.gameObject);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

