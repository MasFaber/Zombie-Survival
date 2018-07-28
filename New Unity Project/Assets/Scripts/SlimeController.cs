using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

    public float MoveSpeed;

    private Rigidbody2D MyRigidBody;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;


	// Use this for initialization

	void Start () {
        MyRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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
}
