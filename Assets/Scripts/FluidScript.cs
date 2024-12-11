using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidScript : MonoBehaviour
{
    public float moveForce = 5.0f;

    public float moveForceUp = 5.0f;

    private bool isGrounded = false;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Test Movement
       if(Input.GetKey(KeyCode.D))
       {
            transform.Translate(Vector2.right * Time.deltaTime * moveForce);
            //rb.velocity = new Vector2(Time.deltaTime * moveForce, 0);
            sr.flipX = false;
       }
       else if (Input.GetKey(KeyCode.A))
       {
            transform.Translate(Vector2.left * Time.deltaTime * moveForce);
            //rb.velocity = new Vector2(-Time.deltaTime * moveForce, 0);
            sr.flipX = true;
       }    
       else 
       {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
       }    

       if(Input.GetKey(KeyCode.W) && isGrounded)
       {
            transform.Translate(Vector2.up * Time.deltaTime * moveForceUp);
            //rb.velocity = new Vector2(rb.velocity.x, Time.deltaTime * jumpForce);
       }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
        isGrounded = true;
        } 
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        } 
    }
}
