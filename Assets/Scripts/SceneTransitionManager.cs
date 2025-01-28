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
        if (Instance != null && Instance != this)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Debug.Log("Reached Main Menu. Destroying old Instance");
                Destroy(Instance.gameObject); // Destroy the old instance entirely
                Instance = this; // Set the current instance
                DontDestroyOnLoad(gameObject); // Make it persist
            }
            else
            {
                Debug.LogError("There is more than one instance! Destroying this instance.");
                Destroy(gameObject); // Destroy the duplicate instance
            }
            return;
        }

        // If Instance is null or this is the first instance
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [Header("Scene Management")]
    private int currentSceneIndex = 0;
    private int sceneIndexToLoad = 0;


    [Header("Transition Effect")]
    public float transitionSpeed = 2.0f;
    private bool initiateTransition = false;
    private bool transitionDone = false;
    private bool sceneWasSwitched = false;

    public void startSinglePlayer()
    {
        currentSceneIndex = 0;
        sceneIndexToLoad = 1;
        initiateTransition = true;
    }

    public void initiateTransitionToNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndexToLoad = (currentSceneIndex + 1);

        if(currentSceneIndex >= 6)
        {
            sceneIndexToLoad = 0;
        }

        initiateTransition = true;
    }

    public void loadMultiPlayerLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndexToLoad = 6;
        initiateTransition = true;
    }

    public void loadMainMenu()
    {
        currentSceneIndex = 0;
        sceneIndexToLoad = 0;
        initiateTransition = true;
    }

    public void quit()
    {
        Application.Quit();
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
        SceneManager.LoadScene(sceneIndexToLoad);
        sceneWasSwitched = true;
    }
}
