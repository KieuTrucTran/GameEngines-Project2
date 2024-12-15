using UnityEngine;

public class WindZone : MonoBehaviour
{
    public Vector2 windDirection = new Vector2(1f, 0f); // Direction of the wind (normalized vector)
    public float windStrength = 5f;                    // Strength of the wind

    private Vector2 windForce;

    void Start()
    {
        // Normalize the wind direction and scale it by strength
        windForce = windDirection.normalized * windStrength;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is entering the wind zone
        if (collision.CompareTag("Player"))
        {
            GasMovement gasMovement = collision.GetComponent<GasMovement>();
            if (gasMovement != null)
            {
                gasMovement.SetWindForce(windForce);
                gasMovement.SetInWindZone(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Reapply wind force in case it's needed
        if (collision.CompareTag("Player"))
        {
            GasMovement gasMovement = collision.GetComponent<GasMovement>();
            if (gasMovement != null)
            {
                gasMovement.SetWindForce(windForce);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset wind effects when the player leaves the wind zone
        if (collision.CompareTag("Player"))
        {
            GasMovement gasMovement = collision.GetComponent<GasMovement>();
            if (gasMovement != null)
            {
                gasMovement.SetInWindZone(false);
            }
        }
    }
}