using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
   //private bool isActivated = false; // Tracks the current state of activation
    private Animator animator; 
    private BoxCollider2D collider;

    private void Start(){
        
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
    
        // Access the vertical velocity from the collision
        float verticalVelocity = collision.relativeVelocity.y;
        float activationThreshold = 11.0f; // Set your threshold value here

        // Check if the vertical velocity exceeds the threshold
        if (Mathf.Abs(verticalVelocity) > activationThreshold)
        {
            Debug.Log("Breaking - Velocity Threshold Met"+verticalVelocity);
            ActivateButton();
            collider.size = Vector3.zero;
            collider.offset = Vector3.zero;
        }
        else
        {
            Debug.Log("Collision did not meet the velocity threshold"+verticalVelocity);
        }
    }

    private void ActivateButton()
    {
        // Toggle the activation state
        animator.SetTrigger("breaking");


    }
}

