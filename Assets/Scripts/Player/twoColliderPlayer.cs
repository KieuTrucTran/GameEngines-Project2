using UnityEngine;

public class twoColliderPlayerScript : MonoBehaviour
{

    public GameObject playerParent;
    twoPlayerTransition playerTransitionScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransitionScript = playerParent.GetComponent<twoPlayerTransition>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerTransitionScript.zoneEntered(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerTransitionScript.zoneExited(collision);
    }
}
