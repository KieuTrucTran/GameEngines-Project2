using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Sprite leverOff;
    public Sprite leverOn;

    public GameObject wallTop;
    public GameObject wallBottom;

    public float openingDistance = 2.5f;

    private bool playerInDistance = false;
    private bool leverPressed = false;

    private float initialPositionY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositionY = wallTop.transform.position.y;
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

        if(leverPressed && wallTop.transform.position.y < initialPositionY + openingDistance)
        {
            wallTop.transform.position += Vector3.up * Time.deltaTime;
            wallBottom.transform.position += Vector3.down * Time.deltaTime;
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
