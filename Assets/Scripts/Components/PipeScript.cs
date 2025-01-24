using UnityEngine;

public class PipeTeleport : MonoBehaviour
{
    [Tooltip("Drag the other pipe here in the Inspector (the second in this pair).")]
    public PipeTeleport otherPipe;

    private bool playerInside;
    private PlayerTransition playerTransition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            playerTransition = collision.GetComponentInParent<PlayerTransition>();
            Debug.Log($"{name}: Player entered pipe area.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            playerTransition = null;
            Debug.Log($"{name}: Player left pipe area.");
        }
    }

    private void Update()
    {
        // Only when player is in the pipe trigger
        if (playerInside && playerTransition != null)
        {
            // Press E to teleport
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Must be Water (index 1)
                if (playerTransition.currentStateIndex == 1)
                {
                    if (otherPipe != null)
                    {
                        TeleportPlayer();
                    }
                    else
                    {
                        Debug.LogWarning($"{name} has no 'otherPipe' assigned!");
                    }
                }
                else
                {
                    Debug.Log("Must be Water form to use this pipe!");
                }
            }
        }
    }

    private void TeleportPlayer()
    {
        Debug.Log($"{name}: Teleporting player to pipe '{otherPipe.name}'");
        // Move the top-level player object
        Transform playerRoot = playerTransition.transform; 
        playerRoot.position = otherPipe.transform.position;
    }
}