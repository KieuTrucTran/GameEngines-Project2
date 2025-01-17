using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public GameObject wallTop;
    public GameObject wallBottom;

    private bool buttonPressed = false;

    private float initialPositionY;

    private float moveSpeedOpen = 0.1f;
    private float moveSpeedClose = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositionY = wallTop.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonPressed)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (wallTop.transform.position.y > initialPositionY)
            {
                wallTop.transform.position -= Vector3.up * Time.deltaTime * moveSpeedClose;
                wallBottom.transform.position -= Vector3.down * Time.deltaTime * moveSpeedClose;
            }
        }

        if (buttonPressed && wallTop.transform.position.y < initialPositionY + 3)
        {
            wallTop.transform.position += Vector3.up * Time.deltaTime * moveSpeedOpen;
            wallBottom.transform.position += Vector3.down * Time.deltaTime * moveSpeedOpen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            buttonPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            buttonPressed = false;
        }
    }
}
