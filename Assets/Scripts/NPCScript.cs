using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject speechText;

    private Vector3 initialScale; 
    private void Start()
    {
        initialScale = transform.localScale;
        speechText.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            turnToPlayer(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            speechText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speechText.SetActive(false);
        }
    }

    void turnToPlayer(GameObject target)
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.y);
        }else
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.y);
        }
    }

}
