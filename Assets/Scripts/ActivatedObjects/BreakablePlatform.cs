using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    // Adjust to your desired velocity threshold
    [SerializeField] private float activationThreshold = 11.0f;

    // Which state index corresponds to "solid"? For example, 0 = Ice/Solid
    [SerializeField] private int solidStateIndex = 0; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Check if the object colliding has a PlayerTransition script.
        //    (i.e., it's the player in some form)
        PlayerTransition playerTransition = collision.collider.GetComponentInParent<PlayerTransition>();

        // 2. If it's the player AND the player is currently in the solid form, then check velocity.
        if (playerTransition != null && playerTransition.currentStateIndex == solidStateIndex)
        {
            float verticalVelocity = collision.relativeVelocity.y;

            if (Mathf.Abs(verticalVelocity) > activationThreshold)
            {
                Debug.Log($"Breaking - velocity threshold met: {verticalVelocity}");
                ActivateBreakingAnimation();
            }
            else
            {
                Debug.Log($"Collision did NOT meet velocity threshold: {verticalVelocity}");
            }
        }
    }

    private void ActivateBreakingAnimation()
    {
        // Trigger the animator
        animator.SetTrigger("breaking");

        // Disable the platform’s collider so the player can fall through
        boxCollider2D.size = Vector2.zero;
        boxCollider2D.offset = Vector2.zero;
    }
}