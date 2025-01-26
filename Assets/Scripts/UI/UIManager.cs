using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    //[SerializeField] private AudioClip gameOverSound;

    [SerializeField] private GameObject pauseScreen;

    private MusicManager musicManager;

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
        gameOverScreen.SetActive(true);
        //SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        // Reset time scale in case the game was paused
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }
    public void Volume(){
        //SceneManager.LoadScene(0);
        MusicManager.instance.ChangeMusicVolume(0.2f);
    }

    public void Quit(){
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only ber executed in the editor)
        #endif
    }


    private void PauseGame(bool status){
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(status);

        Time.timeScale = System.Convert.ToInt32(!status);
    }
}
