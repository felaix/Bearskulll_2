using System.Diagnostics;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private string[] speachlines;
    [SerializeField] private Sprite faceImage;
    [SerializeField] private bool _destroyAfterTrigger;
    [SerializeField] private float textSpeed;

    private Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!DialogBoxText.instance.textStarted)
            {
                DialogBoxText.instance.SetDialog(speachlines, faceImage, textSpeed);
                DialogBoxText.instance.TriggerDialog();
                if (_destroyAfterTrigger)
                    Destroy(gameObject, 0.1f);
            }
        }
    }

    public void DialogCall()
    {
        gameObject?.SetActive(true);

        if (!DialogBoxText.instance.textStarted)
        {
            DialogBoxText.instance.SetDialog(speachlines, faceImage, textSpeed);
            DialogBoxText.instance.TriggerDialog();
            if (_destroyAfterTrigger)
                Destroy(gameObject, 0.1f);
        }
    }

    public void CreateDialogue(string[] lines, Sprite img, float spd)
    {
        dialogue = new Dialogue();
        dialogue.Initialize(lines, img, spd);
        dialogue.TriggerDialogue();
    }
}

public class Dialogue
{
    public string[] TextLines { get; set; }
    public Sprite Face { get; set; }
    public float TextSpeed { get; set; }

    public void Initialize(string[] lines, Sprite img, float spd)
    {
        TextLines = lines;
        Face = img;
        TextSpeed = spd;
    }

    public void TriggerDialogue()
    {
        DialogBoxText.instance.SetDialog(TextLines, Face, TextSpeed);
        DialogBoxText.instance.TriggerDialog();
    }

}


