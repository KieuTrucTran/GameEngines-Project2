using UnityEngine;

public class DeactivationButton : MonoBehaviour
{
[Header("Activation Settings")]
    public GameObject objectToDeactivate; 

    private BoxCollider2D collider;
    

    private void Start(){
        collider = objectToDeactivate.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        DeactivateButton();

    }

    private void DeactivateButton()
    {

        collider.size = Vector3.zero;
        collider.offset = Vector3.zero;

    }
}
