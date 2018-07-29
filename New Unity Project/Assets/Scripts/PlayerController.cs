using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float initialMoveSpeed;
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D MyRigidBody;
    private bool playerMoving =false;
    private bool playerDashing;
    private Vector2 LastMove;



    //dashing
    public KeyCode dashButton= KeyCode.Space;
    public float dashSpeed = 30f;
    private float dashTime;
    public float startDashTime=0.05f;
    private bool Dashing;
    public float dashCooldown=1f;
    private float dashCooldownTime;
    public Vector3 direction;

    //Dash effect
    public GameObject dashEffect;


    //controls
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right=KeyCode.RightArrow;
    public KeyCode left=KeyCode.LeftArrow;

    //audio

    private AudioSource source;
    public AudioClip DashingSound;


    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        MyRigidBody = GetComponent<Rigidbody2D>();
        dashCooldownTime = 0;

        new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update() {

        

        if (Input.GetKeyDown(dashButton) == true && dashCooldownTime <= 0 && playerMoving==true)
        {
            
            moveSpeed = dashSpeed;
            dashTime = startDashTime;
            dashCooldownTime = dashCooldown;
            playerDashing = true;

            source.clip = DashingSound;
            source.Play();

        }

        playerMoving = false;

        if (dashTime <= 0)
        {
            playerDashing = false;
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

            if (moveSpeed == dashSpeed)
            {
                Instantiate(dashEffect, transform.position, Quaternion.identity);
            }
            playerMoving = true;
            LastMove = new Vector2(direction.x, direction.y);
        }


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
        anim.SetBool("PlayerDashing", playerDashing);

    }
    
}
