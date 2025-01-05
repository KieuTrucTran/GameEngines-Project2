using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer; 

    //private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;


    private void Awake(){ 
        body = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //stop rotation
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocity.y);
        
        //Flip the player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = 0.5f*Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale =   new Vector3(-1*0.5f,1*0.5f,1*0.5f);


            body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocity.y);

            body.gravityScale = 1;

    }


    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

}
