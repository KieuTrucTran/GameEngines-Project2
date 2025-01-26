using UnityEngine;

public class H_ButtonScript : MonoBehaviour
{

    public GameObject wallLeft;
    public GameObject wallRight;

    private bool buttonPressed = false;

    private float initialPositionX;

    public float openingDistance = 2;

    private float moveSpeedOpen = 1.0f;
    private float moveSpeedClose = 2.0f;

    private int objectsInCollider = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositionX = wallLeft.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (buttonPressed)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            // closing the walls
            GetComponent<SpriteRenderer>().color = Color.red;
            if (wallLeft.transform.position.x < initialPositionX)
            {
                wallLeft.transform.position -= Vector3.left * Time.deltaTime * moveSpeedClose;
                wallRight.transform.position -= Vector3.right * Time.deltaTime * moveSpeedClose;
            }
        }

        // opening the walls

        if (buttonPressed && wallLeft.transform.position.x > initialPositionX - openingDistance)
        {
            wallLeft.transform.position += Vector3.left * Time.deltaTime * moveSpeedOpen;
            wallRight.transform.position += Vector3.right * Time.deltaTime * moveSpeedOpen;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerParent = collision.gameObject.transform.parent.gameObject;
        if (playerParent != null)
        {
            if (playerParent.tag == "Player")
            {
                objectsInCollider++;
            }
        }

        if (collision.gameObject.tag == "Box")
        {
            objectsInCollider++;
        }

        if (objectsInCollider > 0)
        {
            buttonPressed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject playerParent = collision.gameObject.transform.parent.gameObject;
        if (playerParent != null)
        {
            if (playerParent.tag == "Player")
            {
                objectsInCollider--;
            }
        }

        if (collision.gameObject.tag == "Box")
        {
            objectsInCollider--;
        }

        if (objectsInCollider == 0)
        {
            buttonPressed = false;
        }
    }
}
