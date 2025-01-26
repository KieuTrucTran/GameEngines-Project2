using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    public static MusicManager instance {get; private set; }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void ChangeMusicVolume(float _change){
        float currentVolume = PlayerPrefs.GetFloat("musicVolume");
        currentVolume += _change;

        if (currentVolume >1)
            currentVolume = 0;
        else if (currentVolume<0)
            currentVolume = 1;

        musicSource.volume = currentVolume;

        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}
