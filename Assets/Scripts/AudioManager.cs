using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip clockSound;
    public AudioClip deathSound;

    [Header("Music Toggle")]
    public Toggle musicToggle;
    public TextMeshProUGUI musicText;

    [Header("Sound Toggle")]
    public Toggle soundToggle;
    public TextMeshProUGUI soundText;

    public Color onColor = Color.blue;
    public Color offColor = Color.red;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadSettings();
    }

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        if (!musicSource.isPlaying)
            musicSource.Play();

        musicToggle.onValueChanged.AddListener(OnMusicToggle);
        soundToggle.onValueChanged.AddListener(OnSoundToggle);

        UpdateMusicUI(musicToggle.isOn);
        UpdateSoundUI(soundToggle.isOn);
    }

    //=========================
    // MUSIC
    //=========================

    void OnMusicToggle(bool value)
    {
        musicSource.mute = !value;

        PlayerPrefs.SetInt("Music", value ? 1 : 0);

        UpdateMusicUI(value);
    }

    void UpdateMusicUI(bool on)
    {
        musicToggle.targetGraphic.color = on ? onColor : offColor;
    }

    //=========================
    // SOUND
    //=========================

    void OnSoundToggle(bool value)
    {
        sfxSource.mute = !value;

        PlayerPrefs.SetInt("Sound", value ? 1 : 0);

        UpdateSoundUI(value);
    }

    void UpdateSoundUI(bool on)
    {
        soundToggle.targetGraphic.color = on ? onColor : offColor;
    }

    //=========================
    // PLAY SOUNDS
    //=========================

    public void PlayClock()
    {
        sfxSource.PlayOneShot(clockSound);
    }

    public void PlayDeath()
    {
        sfxSource.PlayOneShot(deathSound);
    }

    //=========================
    // SAVE / LOAD
    //=========================

    void LoadSettings()
    {
        bool music = PlayerPrefs.GetInt("Music", 1) == 1;
        bool sound = PlayerPrefs.GetInt("Sound", 1) == 1;

        musicToggle.isOn = music;
        soundToggle.isOn = sound;

        musicSource.mute = !music;
        sfxSource.mute = !sound;
    }
}
