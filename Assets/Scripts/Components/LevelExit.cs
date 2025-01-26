using UnityEngine;

public class LevelExit : MonoBehaviour
{
    int playerInExit = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameObject.Find("Player 2") != null)
        {
            playerInExit++;

            if(playerInExit == 2)
            {
                SceneTransitionManager.Instance.initiateTransitionToNextScene();
            }


        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneTransitionManager.Instance.initiateTransitionToNextScene();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.Find("Player 2") != null)
        {
            playerInExit--;
        }
    }
}
