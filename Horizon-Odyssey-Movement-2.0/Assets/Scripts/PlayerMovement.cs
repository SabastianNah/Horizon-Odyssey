using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player rb
    private Rigidbody rb;
    //where player respawns
    private Vector3 respawnPoint;

    public Transform orientation;
    
    public float speed = 6.0f;
    public float gravity = 20.0f;
    public float groundDrag;

    public float horizontalInput;
    public float verticalInput;

    public KeyCode jumpKey = KeyCode.Space;
    public bool readyToJump;
    public float jumpCooldown;
    public float jumpForce;
    public bool extraJump;

    public bool onGround;
    public bool onWall;
    public LayerMask whatIsGround;
    public float distToGround;

    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //get and store player rigidbody
        rb = GetComponent <Rigidbody>();
        rb.freezeRotation = true;
        //make player's current position be where they respawn on start
        respawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        readyToJump = true;
        extraJump = false;
        jumpForce = 14.0f;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    private void Update()
    {
        // ground check
        onGround = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f);

        MyInput();
        SpeedControl();

        // handle drag
        if (onGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey))
        {
            if(onGround && readyToJump){
                Jump();
                readyToJump = false;
                extraJump = true;
            }
            if(onWall && !onGround){
                Jump();
                onWall = false;
                extraJump = true;
            }
        }
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(onGround)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        // in air
        else if(!onGround)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Jumpable")){
            onWall = true;
            MyInput();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Jumpable")){
            onWall = false;
        }
    }
}
