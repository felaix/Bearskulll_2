using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] Slider _slider;
    
    void OnEnable()
    {
        _slider.value = AudioListener.volume;
        AudioManager.instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(Val => AudioManager.instance.ChangeMasterVolume(Val));


        AudioManager.instance.ChangeMasterVolume(.5f);
        _slider.value = AudioListener.volume;
    }

}
