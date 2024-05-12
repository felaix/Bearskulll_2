using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_BarCopy : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] Image _HPBarFill;
    [SerializeField] Text _HPtext;
    // Update is called once per frame
    void Update()
    {
        _HPtext.text = _health._curHP.ToString();
        _HPBarFill.fillAmount = (float)_health._curHP / 100f;
    }
}
