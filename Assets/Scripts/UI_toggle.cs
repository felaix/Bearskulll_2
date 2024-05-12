using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_toggle : MonoBehaviour
{
    [SerializeField] bool toggled = true;
    [SerializeField] GameObject toToggle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }
    public void Toggle()
    {
        toggled = !toggled;
        toToggle.SetActive(toggled);
    }
}
