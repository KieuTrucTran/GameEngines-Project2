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
                    TeleportCameraTarget();
                }
                else
                {
                    Debug.Log("You must be in Water form to use this pipe!");
                }
            }
        }
    }

    private void TeleportCameraTarget()
    {
        if (otherPipe == null)
        {
            Debug.LogError($"{name}: No otherPipe assigned!");
            return;
        }

        // We'll teleport the object the camera is actually following
        // (Assuming in CameraScript, 'player' references the collider child)
        GameObject cameraTarget = CameraScript.Instance.player;
        if (cameraTarget == null)
        {
            Debug.LogError("CameraScript.Instance.player is null! Is the camera's 'player' assigned?");
            return;
        }

        // Move the cameraTarget to the other pipe's position
        cameraTarget.transform.position = otherPipe.transform.position;

        // Also update PlayerTransition so you don't snap back next frame
        int stateIndex = playerTransition.currentStateIndex;
        playerTransition.playerStates[stateIndex].position = cameraTarget.transform.position;
        playerTransition.colliderObject.transform.position = cameraTarget.transform.position;

        Debug.Log($"{name}: Teleported the camera target to {otherPipe.name} in the same prefab pair!");
    }
}