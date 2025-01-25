using UnityEngine;

public class Key : MonoBehaviour
{
    // Reference to the door this key unlocks
    //public Door door;

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        // Check if the player interacts with the key
        if (other.CompareTag("Player"))
        {
            // Collect the key and unlock the door
            //door.Unlock();
            Debug.Log("Key collected, door unlocked!");

            // Destroy the key object after collection
            
        }
    }
}
