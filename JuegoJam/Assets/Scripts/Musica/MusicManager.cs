using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip cutSceneMusic;
    public AudioClip gameMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = scene.name;

        if (name.Contains("Menu") || name.Contains("SettingsScene") || name.Contains("CreditsScene"))
        {
            ChangeMusic(menuMusic);
        }
        else if (name.Contains("InitialCutScene"))
        {
            ChangeMusic(cutSceneMusic);
        }
        else
        {
            ChangeMusic(gameMusic);
        }
    }

    void ChangeMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}
