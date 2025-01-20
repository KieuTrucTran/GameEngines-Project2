using UnityEngine;

public class PipeTeleport : MonoBehaviour
{
    public PipeTeleport otherPipe;
    private bool playerInside;
    private PlayerTransition playerTransition; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            playerTransition = collision.GetComponentInParent<PlayerTransition>();
            Debug.Log("Player entered pipe area.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            playerTransition = null;
            Debug.Log("Player left pipe area.");
        }
    }

    private void Update()
    {
        if (playerInside && playerTransition != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Check if player is in Water form (index 1)
                if (playerTransition.currentStateIndex == 1)
                {
                    // Teleport the entire Player parent
                    Transform playerRoot = playerTransition.gameObject.transform;
                    playerRoot.position = otherPipe.transform.position;

                    Debug.Log("Teleported the whole player!");
                }
                else
                {
                    Debug.Log("You must be in Water form to use the pipe.");
                }
            }
        }
    }
}