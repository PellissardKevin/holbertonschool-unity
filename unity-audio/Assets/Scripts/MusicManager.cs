using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    private AudioSource audioSource;

    public AudioClip mainMenuMusic; // Assign these in the Inspector
    public AudioClip levelMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string musicClipName = "";

        if (scene.name == "MainMenu")
        {
            musicClipName = "MainMenuMusic";
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Level01" || scene.name == "Level02" || scene.name == "Level03")
        {
            musicClipName = "LevelMusic";
            PlayMusic(levelMusic);
        }
        else
        {
            StopMusic(); // Stop music for other scenes
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
        {
            return; // No need to play the same clip again
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
