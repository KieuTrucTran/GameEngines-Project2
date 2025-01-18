using UnityEngine;

public class GratesScript : MonoBehaviour
{
    public GameObject playerParent;
    PlayerTransition pt;

    void Start()
    {
        pt = playerParent.GetComponent<PlayerTransition>();    
        Debug.Log(pt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Try to get PlayerTransition from the colliding object (or its parents)
        if (pt != null)
        {
            if (pt.currentStateIndex == 1 || pt.currentStateIndex == 3)
            {
                // Water (1) or Gas (3) -> ignore collisions on ALL player colliders
                IgnorePlayerCollision(collision.gameObject, ignore: true);
                Debug.Log("Player in Water/Gas form -> Passing through grate.");
            }
            else
            {
                // Ice or anything else -> make sure collisions are NOT ignored
                IgnorePlayerCollision(collision.gameObject, ignore: false);
                Debug.Log("Player in Ice form -> Blocked by grate.");
            }
        }
    }

 

    // Turn collision on/off for every collider on the player’s side
    private void IgnorePlayerCollision(GameObject playerObj, bool ignore)
    {
        // Get all colliders in the player's entire hierarchy
        Collider2D[] playerColliders = playerObj.GetComponentsInChildren<Collider2D>(true);

        // Get *this* grate’s collider
        Collider2D grateCollider = GetComponent<Collider2D>();

        // Now ignore or enable collision for each child collider
        foreach (Collider2D pc in playerColliders)
        {
            Physics2D.IgnoreCollision(pc, grateCollider, ignore);
        }
    }

   
}