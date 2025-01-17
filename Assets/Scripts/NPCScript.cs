using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject speechText;


    private void Start()
    {
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
            Debug.Log(collision.gameObject + " entered an NPC's speech range");
            speechText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject + " exited an NPC's speech range");
            speechText.SetActive(false);
        }
    }

    void turnToPlayer(GameObject target)
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
