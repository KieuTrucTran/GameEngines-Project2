using UnityEngine;

public class ActivationButton : MonoBehaviour
{
    [Header("Activation Settings")]
    public GameObject objectToActivate; 

    private bool isActivated = false; // Tracks the current state of activation
    private Animator animator; 

    private void Start(){
        // Check if the button has an Animator component for visual effects
        animator = objectToActivate.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        //Rigidbody2D body = GetComponent<Rigidbody2D>();
        //body.linearVelocity = new Vector2(body.linearVelocity.x, -1);
        if (!isActivated){
            ActivateButton();
            isActivated = true;
        }
    }

    private void ActivateButton()
    {
        // Toggle the activation state
        animator.SetTrigger("lift");

        // Log for debugging
        Debug.Log("Button activated! Current state: " + isActivated);
        
        // Activate or deactivate the target object
        //if (objectToActivate != null)
        //    objectToActivate.SetActive(isActivated);

    }
}
