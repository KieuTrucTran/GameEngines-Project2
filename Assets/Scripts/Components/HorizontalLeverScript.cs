using UnityEngine;

public class HorizontalLeverScript : MonoBehaviour
{
    public Sprite leverOff;
    public Sprite leverOn;

    public GameObject wallLeft;
    public GameObject wallRight;

    public float openingDistance = 2.5f;

    private bool playerInDistance = false;
    private bool leverPressed = false;

    private float initialPositionX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositionX = wallLeft.transform.position.x;
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

        if(leverPressed && wallLeft.transform.position.x > initialPositionX - openingDistance)
        {
            wallLeft.transform.position += Vector3.left * Time.deltaTime;
            wallRight.transform.position += Vector3.right * Time.deltaTime;
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
