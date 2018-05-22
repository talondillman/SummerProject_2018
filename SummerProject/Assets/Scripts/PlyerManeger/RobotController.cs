using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    //public variables
    public float maxSpeed = 10;
    public float jumpforce = 200;
    
    private bool grounded = false;
    private bool facingRight = true;
    private float speed = 0;
    private Rigidbody player;
    
    //Used to see if on the ground
    public Transform groundCheck;
    //Area that it looks for collisions
    public float groundRadius = 0.2f;
    //Used to tell the animator what player can land on.
    public LayerMask whatIsGround;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Checks all variables to see if on ground
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //anim.SetFloat("vSpeed", player.velocity.y);
        //anim.SetBool("Ground", grounded);

        float move = Input.GetAxis("Horizontal");

        //anim.SetFloat("Speed", Mathf.Abs(move));

        player.velocity = new Vector2(move * maxSpeed, player.velocity.y);

        //Flips player if moving other direction
        //if (move > 0 && !facingRight)
        //{
        //    Flip();
        //}
        //else if (move < 0 && facingRight)
        //{
        //    Flip();
        //}
    }
    private void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        transform.Translate(0, 0, 0);
        ////Only allow jump while on ground
        //if (grounded && Input.GetKeyDown(KeyCode.Space))//Hardcoded to space bar
        //{
        //    anim.SetBool("Ground", false);
        //    player.AddForce(new Vector2(0, jumpforce));
        //}
    }
    //Makes sprite face the correct direction
    //void Flip()
    //{
    //    facingRight = !facingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}

    //used to change UI elements
    private void OnGUI()
    {
        //i.e. display health that is current
        //GUI.Label(new Rect(10,10,100,30), "Health: " + health);
    }
}
