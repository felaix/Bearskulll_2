using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private GameObject _waveStartUI;
    [SerializeField] private TMP_Text _waveCompletedTMP;
    [SerializeField] private TMP_Text _waveCounterTMP;

    [Header("Animation")]
    [SerializeField] private float _animDuration = 1f;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private Ease _ease;
    private void Start()
    {
        WaveController.Instance.EndWave += EndWaveUI;
        WaveController.Instance.StartWave += StartWaveUI;
    }

    private void StartWaveUI()
    {
        StartCoroutine(FadeOutImageCoroutine(_waveStartUI.GetComponent<Image>()));
    }

    private IEnumerator FadeOutImageCoroutine(Image img)
    {
        img.gameObject.SetActive(true);
        img.transform.localScale = Vector3.one;
        img.transform.DOScale(Vector3.zero, _animDuration).SetEase(_ease);
        img.DOColor(Color.clear, _animDuration);
        yield return new WaitForSeconds(_delay);
        img.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator FadeInAndOutTMP(TMP_Text tmp)
    {
        tmp.gameObject.SetActive(true);
        tmp.DOColor(Color.green, _animDuration);
        yield return new WaitForSeconds(_delay);
        tmp.DOColor(Color.clear, _animDuration);
        tmp.gameObject.SetActive(false);

        yield return null;
    }

    private IEnumerator UpdateWaveCounter(TMP_Text tmp)
    {

        tmp.gameObject.SetActive(true);
        tmp.DOColor(Color.green, _animDuration);
        yield return new WaitForSeconds(_delay);
        tmp.DOColor(Color.white, _animDuration);
        tmp.text = $"Wave {WaveController.Instance.GetCurrentWaveCount()}";
        yield return null;
    }

    private void EndWaveUI()
    {
        Debug.Log("UI - end wave");
        //if (WaveController.Instance.GetCurrentWaveCount() == 0) return;

        StartCoroutine(FadeInAndOutTMP(_waveCompletedTMP));
        StartCoroutine(UpdateWaveCounter(_waveCounterTMP));
    }
}
