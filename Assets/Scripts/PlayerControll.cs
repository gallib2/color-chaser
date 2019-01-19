using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour 
{
    //public delegate void BlackPlatformAction();
    public static event Action OnTouchBlackPlatform;

    public static event Action OnStopPainting;


    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilstoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;
    private float jumpForceStore;

    public float jumpForceMultiplierGreen;
    public float jumpForceMultiplierRed;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDublejump;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    public ChaserGameManager theGameManager;

   // private Collider2D myCollider;

    private Animator myAnimator;
    public float blackPlatformEffectTime = 3.0f;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        //myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;
        jumpForceStore = jumpForce;

        speedMilstoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilstoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
    }
	
	// Update is called once per frame
	void Update () {

        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if(transform.position.x > speedMilstoneCount)
        {
            speedMilstoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed *= speedMultiplier;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false; 
            }

            if(!grounded && canDublejump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDublejump = false;
            }

        }

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
            canDublejump = true;
        }


        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        jumpForce = jumpForceStore;

        if (other.gameObject.tag == "killBox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            speedMilstoneCount = speedMilestoneCountStore;
        }

        if(other.gameObject.tag == "GreenPlatform")
        {
            jumpForce = jumpForce * jumpForceMultiplierGreen;
        }

        if(other.gameObject.tag == "RedPlatform")
        {
            if(jumpForceMultiplierRed > 0)
            {
                jumpForce = jumpForce / jumpForceMultiplierRed;
            }
        }

        if (other.gameObject.tag == "BlackPlatform")
        {
            OnPlayerTouchBlackPlatform();
        }
    }

    private void OnPlayerTouchBlackPlatform()
    {
        StartCoroutine("PlayerTouchBlackPlatformCo");
    }

    public IEnumerator PlayerTouchBlackPlatformCo()
    {
        if (OnTouchBlackPlatform != null)
        {
            OnTouchBlackPlatform();
        }

        yield return new WaitForSeconds(blackPlatformEffectTime);

        if (OnStopPainting != null)
        {
            OnStopPainting();
        }
    }

}
