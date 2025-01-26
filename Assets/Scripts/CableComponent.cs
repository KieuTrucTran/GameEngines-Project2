using UnityEngine;

public class CableComponent : MonoBehaviour
{

    public GameObject wallTop;
    public GameObject wallBottom;

    private bool zoneEntered = false;

    private float initialPositionY;

    private float moveSpeedOpen = 1.0f;
    private float moveSpeedClose = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPositionY = wallTop.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneEntered && wallTop.transform.position.y < initialPositionY + 2)
        {
            wallTop.transform.position += Vector3.up * Time.deltaTime * moveSpeedOpen;
            wallBottom.transform.position += Vector3.down * Time.deltaTime * moveSpeedOpen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject playerParent = collision.gameObject.transform.parent.gameObject;
        if (playerParent != null)
        {
            if (playerParent.tag == "Player")
            {
                PlayerTransition playerScript = collision.gameObject.GetComponentInParent<PlayerTransition>();
                if (playerScript != null)
                {
                    if (playerScript.currentStateIndex == 1) zoneEntered = true;
                }
            }
        }
    }

}
