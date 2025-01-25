using UnityEngine;

public class PipeScript : MonoBehaviour
{
    [Tooltip("Reference to the other child pipe in this same prefab.")]
    public PipeScript otherPipe;

    private bool playerInside;
    private PlayerTransition playerTransition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            playerTransition = collision.GetComponentInParent<PlayerTransition>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            playerTransition = null;
        }
    }

    private void Update()
    {
        if (playerInside && playerTransition != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) // your chosen interaction key
            {
                // Must be Water form (stateIndex = 1)
                if (playerTransition.currentStateIndex == 1)
                {
                    TeleportPlayer();
                }
                else
                {
                    Debug.Log("You must be in Water form to use this pipe!");
                }
            }
        }
    }

    private void TeleportPlayer()
    {
        if (otherPipe == null)
        {
            Debug.LogError($"{name}: No otherPipe assigned!");
            return;
        }

        // Move the entire top-level player object
        Transform playerRoot = playerTransition.transform;
        playerRoot.position = otherPipe.transform.position;

        // If PlayerTransition snaps the player each frame, update it too:
        playerTransition.playerStates[playerTransition.currentStateIndex].position = playerRoot.position;
        playerTransition.colliderObject.transform.position = playerRoot.position;

        Debug.Log($"{name}: Teleported player to {otherPipe.name} in the same prefab pair!");
    }
}