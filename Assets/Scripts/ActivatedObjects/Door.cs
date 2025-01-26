using UnityEngine;

public class Door : MonoBehaviour
{
    private bool unlocked;


    void Start()
    {
        unlocked = false;
    }
    public void Unlock(){
        unlocked = true;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        
        if(unlocked)
            gameObject.SetActive(false);
        
    }
}
