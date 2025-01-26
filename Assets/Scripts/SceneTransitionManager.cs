using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTransitionManager : MonoBehaviour
{

    public bool debugMode = true;

    // Singelton Class
    public static SceneTransitionManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Scene Management")]
    public string[] scenes;
    private int currentSceneIndex = 0;


    [Header("Transition Effect")]
    public float transitionSpeed = 2.0f;
    private bool initiateTransition = false;
    private bool transitionDone = false;
    private bool sceneWasSwitched = false;

    void Start()
    {
        if (debugMode)
        {
            Debug.Log("Starting up SceneManager...");
            Debug.Log(Instance);
            Debug.Log("Scene List:");
            foreach (var s in scenes)
            {
                if (s == scenes[currentSceneIndex]) {
                    Debug.Log(">" + s);
                }else
                {
                    Debug.Log(s);
                }
                
            }
        }
    }

    public void initiateTransitionToNextScene()
    {
        if(currentSceneIndex == 5) currentSceneIndex = 0;

        initiateTransition = true;

        if (debugMode) Debug.Log("Starting transition from " + scenes[currentSceneIndex] + " to next scene");
    }

    public void loadMultiPlayerLevel()
    { 
        currentSceneIndex = 6;
        initiateTransition = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkTransitionState();

        if (transitionDone)
        {
            initiateTransition = false;
            if (debugMode) Debug.Log("Loading next scene");
            loadToNextScene();
        }

        if (sceneWasSwitched)
        {
            transitionDone = false;
        }
    }

    /*
     * @Value "_CutOff" in shader is step operation applied on gradient image
     * - (-1) is completely black (means transition can start)
     * - (1) is completely transparent
     */
    void checkTransitionState()
    {
        GameObject imageObject = GameObject.Find("Canvas/TransitionImage");
        Image image = imageObject.GetComponent<Image>();

        if (initiateTransition)
        {
            image.material.SetFloat("_CutOff",
                Mathf.MoveTowards(image.material.GetFloat("_CutOff"), -1, transitionSpeed * Time.deltaTime));
        }
        else
        {
            image.material.SetFloat("_CutOff",
                Mathf.MoveTowards(image.material.GetFloat("_CutOff"), 1, transitionSpeed * Time.deltaTime));
        }

        float cutOffValue = image.material.GetFloat("_CutOff");
        if (cutOffValue == -1)
        {
            transitionDone = true;
        }
    }

    void loadToNextScene()
    {
        transitionDone = false;
        string currentSceneName = scenes[currentSceneIndex];
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
        sceneWasSwitched = true;
        currentSceneIndex++;

        Debug.Log("Scene List:");
        foreach (var s in scenes)
        {
            if (s == scenes[currentSceneIndex])
            {
                Debug.Log(">" + s);
            }
            else
            {
                Debug.Log(s);
            }

        }
    }
}
