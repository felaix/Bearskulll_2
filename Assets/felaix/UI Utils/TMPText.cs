using BayatGames.SaveGameFree;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPText : MonoBehaviour
{
    private TMP_Text tmp;

    [SerializeField] private string englishText;
    [SerializeField] private string germanText;

    private void Start()
    {
        tmp = GetComponent<TMP_Text>();

        if (SaveGame.Load<string>("Language") == "German")
        {
            tmp.text = germanText;
        }
        else tmp.text = englishText;
    }


}
