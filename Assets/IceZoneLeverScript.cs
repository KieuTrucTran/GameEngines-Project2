using UnityEngine;

public class IceZoneLeverScript : MonoBehaviour
{
    public Sprite leverOff;
    public Sprite leverOn;

    // The ColdZone object you want to deactivate
    public GameObject coldZone;

    private bool playerInDistance = false;
    private bool leverPressed = false;

    private float initialPositionX; // Not strictly needed if we just deactivate

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If needed, store the initial X position of the ColdZone (matching the original code style)
        if (coldZone != null)
        {
            initialPositionX = coldZone.transform.position.x;
        }

        // Ensure we start with the leverOff sprite if assigned
        if (leverOff != null)
        {
            GetComponent<SpriteRenderer>().sprite = leverOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is near the lever but hasn't pressed it yet...
        if (playerInDistance && !leverPressed)
        {
            // Check for input
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverPressed = true;
                if (leverOn != null)
                {
                    GetComponent<SpriteRenderer>().sprite = leverOn;
                }
            }
        }

        // Once pressed, simply deactivate the ColdZone
        if (leverPressed && coldZone != null)
        {
            coldZone.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInDistance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInDistance = false;
        }
    }
}