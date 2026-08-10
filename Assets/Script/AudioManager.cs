using UnityEngine;

// Simple singleton for one-shot SFX. Put this on an empty GameObject
// in your scene (with an AudioSource, or it'll add one automatically),
// assign clips in the Inspector. Other scripts call AudioManager.Instance.PlayX().
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    public AudioClip checkpointClip;
    public AudioClip diamondClip;
    public AudioClip crashClip;
    public AudioClip gameOverClip;

    private AudioSource source;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        source = GetComponent<AudioSource>();
        if (source == null) source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayCheckpoint() => PlayOneShot(checkpointClip);
    public void PlayDiamond() => PlayOneShot(diamondClip);
    public void PlayCrash() => PlayOneShot(crashClip);
    public void PlayGameOver() => PlayOneShot(gameOverClip);

    void PlayOneShot(AudioClip clip)
    {
        if (clip != null) source.PlayOneShot(clip);
    }
}

