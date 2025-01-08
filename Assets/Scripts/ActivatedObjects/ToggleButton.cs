using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [Header("Activation Settings")]
    public GameObject objectToActivate; 

    private bool isActivated = false; // Tracks the current state of activation
    private Animator animator; 

    private void Start()
    {
        // Check if the button has an Animator component for visual effects
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            isActivated = true;
            ActivateButton();
        }
    }

    private void ActivateButton()
    {
        // Toggle the activation state
        animator.SetTrigger("toggle");

        // Log for debugging
        Debug.Log("Button activated! Current state: " + isActivated);
        
        // Activate or deactivate the target object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(isActivated);
            if (isActivated)
        {
            // Move the object to the left
            Rigidbody2D body = objectToActivate.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = new Vector2(-1, body.linearVelocity.y);
            }
            else
            {
                Debug.LogWarning("No Rigidbody2D found on the object!");
            }
        }
        }


    }
}
