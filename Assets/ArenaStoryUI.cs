using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class ArenaStoryUI : MonoBehaviour
{

    private Image img;

    public MenuManager menuManager;

    [Header("Fading")]
    public float FadeInSpd;
    public float FadeOutSpd;
    public Ease fadingEase;

    [SerializeField] private Button exitBtn;
    [SerializeField] private Button continueBtn;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    private void OnEnable()
    {
        img.color = Color.clear;
        img.DOFade(1, FadeInSpd).SetEase(fadingEase);

        //WaveController.Instance.StopWave();

        exitBtn.onClick.AddListener(() => Exit());
        continueBtn.onClick.AddListener(() => Continue());
    }

    private void Continue()
    {
        Debug.Log("Continue here");
        img.DOFade(0, FadeOutSpd).SetEase(fadingEase);
        Invoke("DeactivateGameObjectInvoker", FadeOutSpd);
    }

    private void DeactivateGameObjectInvoker()
    {
        gameObject.SetActive(false);
    }

    private void Exit()
    {
        menuManager.ExitGame();
    }

}
