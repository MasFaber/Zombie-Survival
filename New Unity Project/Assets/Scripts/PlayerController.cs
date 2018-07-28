using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float initialMoveSpeed;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D MyRigidBody;
    private bool playerMoving;
    private Vector2 LastMove;



    //dashing
    public KeyCode dashButton;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool Dashing;
    public float dashCooldown;
    private float dashCooldownTime;
    public Vector3 direction;


    //controls
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;



    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        MyRigidBody = GetComponent<Rigidbody2D>();
        dashCooldownTime = 0;

        new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(dashButton) == true && dashCooldownTime <= 0)
        {
            moveSpeed = dashSpeed;
            dashTime = startDashTime;
            dashCooldownTime = dashCooldown;
        }

        if (dashTime <= 0)
        {
            playerMoving = false;
            moveSpeed = initialMoveSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        dashCooldownTime -= Time.deltaTime;

        if (Input.GetKey(up)) { direction += Vector3.up; }
        if (Input.GetKey(down)) { direction += Vector3.down; }
        if (Input.GetKey(left)) { direction += Vector3.left; }
        if (Input.GetKey(right)) { direction += Vector3.right; }

        direction.Normalize();

        if (direction != Vector3.zero)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            playerMoving = true;
            LastMove = new Vector2(direction.x, direction.y);
        }

        //if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        //{
        //    transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        //    playerMoving = true;
        //    LastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        //}


        //if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        //{
        //    //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        //    MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
        //    playerMoving = true;
        //    LastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        //}

        else
        {
            //Stop moving (for Rigidbody)
            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                MyRigidBody.velocity = new Vector2(0f, MyRigidBody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, 0f);
            }
        }

        direction=Vector3.zero;

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY",Input.GetAxisRaw("Vertical"));

        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", LastMove.x);
        anim.SetFloat("LastMoveY", LastMove.y);

    }
    
}
