using UnityEngine;

public class Key : MonoBehaviour
{
    
    public Door door;
    private void OnTriggerEnter2D(Collider2D collision){
        
            door.Unlock();
            gameObject.SetActive(false);
        
    }
}
