using UnityEngine;


[DefaultExecutionOrder(0)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public bool iscutszene;
    [SerializeField] public AudioSource _music, _effect;

    public bool isHub = false;
    public AudioClip HubMusic;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ChangeMasterVolume(.5f);
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        if (isHub)
        {
            Invoke("startmusic", 0.1f);

        }
    }
    void startmusic()
    {
        PlayMusic(HubMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        _music.clip = clip;
        _music.PlayDelayed(1);
    }


    public void PlayEffect(AudioClip clip)
    {
        _effect.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;

        _music.volume = value;
        _effect.volume = value;
    }

    public void MusicToggle()
    {
        _music.mute = !_music.mute;
    }
    public void EffectToggle()
    {
        _effect.mute = !_effect.mute;
    }


}
