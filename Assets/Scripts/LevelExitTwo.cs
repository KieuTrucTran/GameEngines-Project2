using UnityEngine;

public class LevelExitTwo : MonoBehaviour
{
    private int playersInExit = 0; // Tracks the number of players in the exit
    private const int requiredPlayers = 2; // The number of players required to trigger the transition

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInExit++;
            Debug.Log($"Player entered the exit. Players in exit: {playersInExit}");

            // Check if all required players are in the exit
            if (playersInExit == requiredPlayers)
            {
                Debug.Log("All players in the exit. Transitioning to the next scene.");
                SceneTransitionManager.Instance.initiateTransitionToNextScene();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersInExit--;
            Debug.Log($"Player exited the exit. Players in exit: {playersInExit}");

            // Ensure playersInExit does not go below 0
            if (playersInExit < 0)
            {
                playersInExit = 0;
                Debug.LogWarning("Players in exit count was below 0. Resetting to 0.");
            }
        }
    }
}
