using UnityEngine;

public class AudioPlayOnStart : MonoBehaviour
{

    [SerializeField] AudioClip _clip;
    [SerializeField] bool _isMusic;


    void Start()
    {
        if(!_isMusic)
            AudioManager.instance.PlayEffect(_clip);
        else
            AudioManager.instance.PlayMusic(_clip);

    }

}
