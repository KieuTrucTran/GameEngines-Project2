using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    //[SerializeField] private AudioClip gameOverSound;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject sceneList;

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
        //SceneManager.LoadScene(0);
        pauseScreen.SetActive(false);
        sceneList.SetActive(true);
    }

    public void LoadScene1(){
        // Reset time scale in case the game was paused
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void LoadScene2(){
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void LoadScene3(){
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void LoadScene4(){
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }
    public void LoadScene5(){
        Time.timeScale = 1;
       SceneManager.LoadScene(4);
    }
    public void LoadScene6(){
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
    }


    public void Volume(){
        //SceneManager.LoadScene(0);
        MusicManager.instance.ChangeMusicVolume(0.1f);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
    }


    private void PauseGame(bool status){
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(status);

        Time.timeScale = System.Convert.ToInt32(!status);
    }
}
