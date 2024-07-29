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

    public AudioClip level02Music; // Add new clip for Level02
    public AudioClip level03Music; // Add new clip for Level03

    public AudioMixer masterMixer;  // Reference to the master AudioMixer
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot pausedSnapshot;

    private const string BGM_VOLUME_KEY = "BGMVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume"; // Key for SFX volume
    private const float DEFAULT_VOLUME = 1f;  // Default volume level if not set


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Load and apply saved BGM volume
        float savedBGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, DEFAULT_VOLUME);
        SetBGMVolume(savedBGMVolume);

        // Load and apply saved SFX volume
        float savedSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_VOLUME);
        SetSFXVolume(savedSFXVolume);
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
            case "Options":
                PlayMusic(mainMenuMusic); // Use appropriate clip based on scene
                break;
            case "Level01":
                PlayMusic(levelMusic);
                break;
            case "Level02":
                PlayMusic(level02Music);  // Play Level02 music
                break;
            case "Level03":
                PlayMusic(level03Music);  // Play Level03 music
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

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
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
    }

    public void ResumeGame()
    {
        if (defaultSnapshot != null)
        {
            defaultSnapshot.TransitionTo(0f); // Transitions back to the default snapshot
        }
    }


    public void SetBGMVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat(SFX_VOLUME_KEY, Mathf.Log10(volume) * 20); // Convert to dB
        }
    }

    public void EnsureDefaultSnapshot()
    {
        ResumeGame();
    }
}
