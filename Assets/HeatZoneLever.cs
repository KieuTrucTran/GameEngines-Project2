using UnityEngine;

public class HeatZoneLeverScript : MonoBehaviour
{
    public Sprite leverOff;
    public Sprite leverOn;

    // The HeatZone object you want to deactivate
    public GameObject heatZone;

    private bool playerInDistance = false;
    private bool leverPressed = false;

    private float initialPositionX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Store the HeatZone's initial X position just to mirror the example's logic
        // (Not strictly needed if we only deactivate, but included to match structure)
        if (heatZone != null)
        {
            initialPositionX = heatZone.transform.position.x;
        }

        // Optional: if you have a leverOff sprite, ensure it's shown initially
        if (leverOff != null)
        {
            GetComponent<SpriteRenderer>().sprite = leverOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is within interaction distance...
        if (playerInDistance)
        {
            // Check for lever pull (press E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverPressed = true;
                // Change sprite to indicate lever on
                if (leverOn != null)
                {
                    GetComponent<SpriteRenderer>().sprite = leverOn;
                }
            }
        }

        // Once pressed, disable the HeatZone (one-time action)
        if (leverPressed && heatZone != null)
        {
            heatZone.SetActive(false);
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