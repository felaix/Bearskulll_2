using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] bool _toggleMusic, _toggleEffects;
    [SerializeField] GameObject _toggleImage;
    private bool _togglebool = true;
    public void AudioToggled()
    {
        _togglebool = !_togglebool;
        if (_toggleMusic) AudioManager.instance.MusicToggle();
        if (_toggleEffects) AudioManager.instance.EffectToggle();
        _toggleImage.SetActive(_togglebool);
    }

    private void OnEnable()
    {
        if (_toggleMusic) _toggleImage.SetActive(!AudioManager.instance._music.mute);
        if (_toggleEffects) _toggleImage.SetActive(!AudioManager.instance._effect.mute);
    }

}
