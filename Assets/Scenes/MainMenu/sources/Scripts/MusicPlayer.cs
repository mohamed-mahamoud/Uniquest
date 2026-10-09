using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private const string VolumeKey = "MusicVolume";
    private const float DefaultVolume = 0.7f;

    public static MusicPlayer Instance { get; private set; }
    public static float SavedVolume => PlayerPrefs.GetFloat(VolumeKey, DefaultVolume);

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = SavedVolume;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}