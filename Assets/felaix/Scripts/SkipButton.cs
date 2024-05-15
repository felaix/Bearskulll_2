using BayatGames.SaveGameFree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{

    private Button btn;
    private TMP_Text tmp;
    [SerializeField] private CinematicPlayerControlltrigger dollyCam;
    [SerializeField] private CinematicTrigger director;

    private void Update()
    {
        if (director == null) return;

        if (director._triggerSet) btn.interactable = false;
        else btn.interactable = true;
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SkipCinematic);

        if (dollyCam == null) FindFirstObjectByType<CinematicPlayerControlltrigger>();
        if (director == null) FindFirstObjectByType<CinematicTrigger>();

        tmp = GetComponentInChildren<TMP_Text>();
        tmp.text = SaveGame.Load<string>("Language") == "English" ? "Skip" : "Überspringen";
    }

    public void SkipCinematic()
    {
        if (dollyCam != null) dollyCam.giveControlls();
        if (director != null) director.SkipCinematic();
        gameObject.SetActive(false);
    }
}
