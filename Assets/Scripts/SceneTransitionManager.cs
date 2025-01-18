using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    // Singelton Class
    public static SceneTransitionManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }
    }

    [Header ("Scene Management")]
    public string[] scenes;
    private int currentSceneIndex = 0;


    [Header("Transition Effect")]
    public Image[] images; // png images containing gradients
    public Image defaultImage;
    public float transitionSpeed = 2.0f;
    private bool hideScene = false;
    private bool transitionDone = false;
    private bool sceneWasSwitched = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            hideScene = !hideScene;
        }

        checkTransitionState();

        if(transitionDone)
        {
            hideScene = false;
            loadToNextScene();
        }

        if(sceneWasSwitched)
        {
            hideScene = false;
        }
    }

    /*
     * @Value "_CutOff" in shader is step operation applied on gradient image
     * - (-1) is completely black (means transition can start)
     * - (1) is completely transparent
     */
    void checkTransitionState()
    {
        Image image = getCurrentImage();
        
        if (hideScene)
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
    }

    Image getCurrentImage()
    {
        Image image = images[currentSceneIndex];
        if (image != null) image = defaultImage;
        return image;
    }
}
