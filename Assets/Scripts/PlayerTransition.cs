using System.Collections;
using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    private int stateCounter = 0;

    //ice, water, water particles, Gas
    public Transform[] playerStates = new Transform[4];

    Vector3 currentPosition = new Vector3(0, 0, 0);
    int currentStateIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = playerStates[currentStateIndex].position;

        //water particles always need to be near player because they can't teleport instantly
        playerStates[2].position = currentPosition;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentStateIndex = 0;

            deactivateAll();
            playerStates[0].gameObject.SetActive(true);
            playerStates[0].position = currentPosition;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentStateIndex = 1;

            deactivateAll();
            playerStates[1].gameObject.SetActive(true);
            playerStates[1].position = currentPosition;
            playerStates[2].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentStateIndex = 3;

            deactivateAll();
            playerStates[3].gameObject.SetActive(true);
            playerStates[3].position = currentPosition;
        }
    }

    private void deactivateAll()
    {
        foreach (Transform t in playerStates)
        {
            t.gameObject.SetActive(false);
        }
    }
}