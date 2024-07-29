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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Load and apply saved BGM volume
        float savedBGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, DEFAULT_VOLUME);
        SetBGMVolume(savedBGMVolume);

        // Load and apply saved SFX volume
        float savedSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, DEFAULT_VOLUME);
        SetSFXVolume(savedSFXVolume);
    }

    // BGM Volume Management
    public void SetBGMVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // SFX Volume Management
    public void SetSFXVolume(float volume)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Convert to dB
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

    public void EnsureDefaultSnapshot()
    {
        ResumeGame();
    }
}
