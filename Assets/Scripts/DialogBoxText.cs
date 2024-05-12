using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxText : MonoBehaviour
{
    public static DialogBoxText instance;

    private void Awake()
    {
        if (DialogBoxText.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Textrefrence
    public Text dialogText;

    //Input Text
    public string[] lines;

    public float textspeed;

    private int index;
    public bool textStarted = false;
    
    public Image face;
    public CanvasGroup cg;

   


    void Start()
    {
        dialogText.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            SkipDialog();
    }

    public void TriggerDialog()
    {
        textStarted = true;
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
        StartDialog();  
    }

    public void SkipDialog()
    {
        
        if (dialogText.text == lines[index])
        {
            NextLine();
            
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[index];
         
        }
    }

    public void SetDialog(string[] npclines, Sprite npcface, float TextSpeed)
    {
        textspeed = TextSpeed;
        face.enabled = true;
        lines = npclines;
        if(npcface != null)
            face.sprite = npcface;
        else
            face.enabled = false;
    }


    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(1/textspeed);
        }
    }

    
    void NextLine()
    {
        if(index < lines.Length -1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textStarted = false;
            dialogText.text = string.Empty;
            lines = new string[0];
            face.enabled = false;
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            cg.interactable = false;
            
        }
    }

}
