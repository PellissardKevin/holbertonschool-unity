using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientAudio : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private AudioSource audioSource;
    public float maxDistance = 20f; // Maximum distance for the audio to be heard

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this GameObject.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        // Ensure the audio is initially set according to the distance
        UpdateAudioVolume();
    }

    void Update()
    {
        UpdateAudioVolume();
    }

    void UpdateAudioVolume()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned.");
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        audioSource.volume = volume;
    }
}
