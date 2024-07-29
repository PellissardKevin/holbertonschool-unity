using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    private AudioSource audioSource;

    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    public AudioClip victoryMusic;

    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot pausedSnapshot;

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const float DEFAULT_VOLUME = 1f;  // Default volume level if not set

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

        // Load saved BGM volume and apply it
        float savedVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, DEFAULT_VOLUME);
        SetVolume(savedVolume);
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
        switch (scene.name)
        {
            case "MainMenu":
            case "OptionsMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "Level01":
            case "Level02":
            case "Level03":
                PlayMusic(levelMusic);
                break;
            default:
                StopMusic();
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Music clip is not assigned.");
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PauseGame()
    {
        if (pausedSnapshot != null)
        {
            pausedSnapshot.TransitionTo(0f); // Transitions to the paused snapshot
        }
        else
        {
            Debug.LogWarning("Paused Snapshot is not assigned.");
        }
    }

    public void ResumeGame()
    {
        if (defaultSnapshot != null)
        {
            defaultSnapshot.TransitionTo(0f); // Transitions back to the default snapshot
        }
        else
        {
            Debug.LogWarning("Default Snapshot is not assigned.");
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // Ensure snapshot transitions to default
    public void EnsureDefaultSnapshot()
    {
        ResumeGame();
    }
}
