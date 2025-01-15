using System.Collections;
using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    private int stateCounter = 0;

    //ice, water, water particles, Gas
    public Transform[] playerStates = new Transform[4];

    Vector3 currentPosition = new Vector3(0, 0, 0);
    int currentStateIndex = 0;

    bool inHeatZone = false;

    bool solidDisabled = false;
    bool gasDisabled = false;

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
            if(!solidDisabled) activateState(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activateState(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(!gasDisabled) activateState(3); //skipped 2, which are the particles
        }
    }

    private void activateState(int state)
    {
        currentStateIndex = state;
        deactivateAll();

        switch (state)
        {
            case 0:
                playerStates[0].gameObject.SetActive(true);
                playerStates[0].position = currentPosition;
                break;
            case 1:
                playerStates[1].gameObject.SetActive(true);
                playerStates[1].position = currentPosition;
                playerStates[2].gameObject.SetActive(true);
                break;
            case 3:
                playerStates[3].gameObject.SetActive(true);
                playerStates[3].position = currentPosition;
                break;
        }
    }

    private void deactivateAll()
    {
        foreach (Transform t in playerStates)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void zoneEntered(Collider2D collider)
    {
        if (collider.gameObject.tag == "Heat")
        {
            solidDisabled = true;
            if (currentStateIndex == 0) activateState(1);
        }

        if(collider.gameObject.tag == "Cold")
        {
            gasDisabled = true;
            if (currentStateIndex == 3) activateState(1);
        }
    }

    public void zoneExited(Collider2D collider)
    {
        if (collider.gameObject.tag == "Heat")
        {
            solidDisabled = false;
        }

        if (collider.gameObject.tag == "Cold")
        {
            gasDisabled = false;
        }
    }
}