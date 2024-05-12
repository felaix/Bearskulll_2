using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private int _goToSceneID;

    public void SwitchScene()
    {
        SceneManager.LoadSceneAsync(_goToSceneID);
    }
}
