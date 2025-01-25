using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    //[SerializeField] private GameObject gameOverScreen;
    //[SerializeField] private AudioClip gameOverSound;

    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        //gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void GameOver(){
        //gameOverScreen.SetActive(true);
        //SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only ber executed in the editor)
        #endif
    }


    private void PauseGame(bool status){
        pauseScreen.SetActive(status);

        Time.timeScale = System.Convert.ToInt32(!status);
    }
}
