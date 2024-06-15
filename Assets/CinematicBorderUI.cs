using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CinematicBorderUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _delay = 2f;
    private RectTransform _rect;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _rect.sizeDelta = new Vector2(0, 0);

        StartCoroutine(CinematicBorderCoroutine());
    }

    public void ManipulateBorderSize(RectTransform rect)
    {
        if (rect.sizeDelta == Vector2.zero) { rect.DOSizeDelta(new Vector2(0, 50), 4f); }
        else { rect.DOSizeDelta(Vector2.zero, 5f); }
    }

    private IEnumerator CinematicBorderCoroutine()
    {
        ManipulateBorderSize(_rect);

        yield return new WaitForSeconds(_delay);

        ManipulateBorderSize(_rect);
    }


}
