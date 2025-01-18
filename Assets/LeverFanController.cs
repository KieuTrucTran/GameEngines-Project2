using UnityEngine;

public class LeverFanController : MonoBehaviour
{
    public Sprite leverOff;
    public Sprite leverOn;

    private bool playerInDistance = false;
    private bool leverPressed = false;

    public GameObject windZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverPressed = true;
                GetComponent<SpriteRenderer>().sprite = leverOn;
            }
        }    

        if (leverPressed) {
            windZone.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInDistance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInDistance = false;
        }
    }
}
