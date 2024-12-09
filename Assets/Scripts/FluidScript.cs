using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidScript : MonoBehaviour
{
    public GameObject particleContainer;
    List<GameObject> particles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Test Movement
       if(Input.GetKey(KeyCode.D))
       {
            transform.Translate(new Vector3(Time.deltaTime, 0, 0));
       }
       if (Input.GetKey(KeyCode.A))
       {
            transform.Translate(new Vector3(-Time.deltaTime, 0, 0));
       }
        foreach (GameObject part in particles)
        {
        }
    }
}
