using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransition : MonoBehaviour
{
    //ice, water, water particles, Gas
    public Transform[] playerStates = new Transform[4];

    public GameObject colliderObject;

    Vector3 currentPosition = new Vector3(0, 0, 0);
    public int currentStateIndex = 0;

    bool solidDisabled = false;
    bool gasDisabled = false;

    [SerializeField] private Image thermometer;
    private Animator animator; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = thermometer.GetComponent<Animator>();
        animator.SetBool("goIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = playerStates[currentStateIndex].position;
        colliderObject.transform.position = currentPosition;


        //water particles always need to be near player because they can't teleport instantly
        playerStates[2].position = currentPosition;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!solidDisabled) activateState(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activateState(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!gasDisabled) activateState(3); //skipped 2, which are the particles
        }


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Get state info for the first layer
        if (stateInfo.IsName("Thermometer_DeFreeze") && stateInfo.normalizedTime >= 1.0f)
        {
            animator.SetBool("goIdle", true);
                //animator.ResetTrigger("freezing");
        } else if (stateInfo.IsName("Thermometer_DeHeat") && stateInfo.normalizedTime >= 1.0f){
            animator.SetBool("goIdle", true);
                //animator.ResetTrigger("heating");
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
            animator.SetTrigger("heating");
            animator.SetBool("goIdle",false);
            //animator.ResetTrigger("deheating");
            Debug.Log("Heat entered");
            
            solidDisabled = true;
            if (currentStateIndex == 0) activateState(1);
        }

        if (collider.gameObject.tag == "Cold")
        {
            animator.SetTrigger("freezing");
            animator.SetBool("goIdle",false);
            //animator.ResetTrigger("defreezing");
            
            gasDisabled = true;
            if (currentStateIndex == 3) activateState(1);
        }
    }
    public void zoneEnxited(Collider2D collider)
    {
       

        if (collider.gameObject.tag == "Heat")
        {
            animator.SetTrigger("deheating");
            
            solidDisabled = false;
        }

        if (collider.gameObject.tag == "Cold")
        {
            gasDisabled = false;
            animator.SetTrigger("defreezing");
        }
    }

}