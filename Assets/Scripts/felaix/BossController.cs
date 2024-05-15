using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance { get; private set; }

    private int state = 0;
    private GameObject player;

    private bool active = false;

    // ? MODEL IS NEEDED TO MOVE THE BOSS
    private Transform model;
    public GameObject areaDamageFX;
    public GameObject nukeFX;
    public GameObject stunnedFX;

    private Animator animator;

    private CameraController cam;
    private Health hp;
    private Fighter fighter;

    [SerializeField] private DialogSystem dialogueSystem;
    [SerializeField] private Sprite witchFace;
    [SerializeField] private List<Health> shieldList;
    [SerializeField] private Transform shieldsParent;

    private bool isEnglish = false;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        model = transform.GetChild(0).GetComponent<Transform>();
        animator = model.GetComponent<Animator>();
        cam = CameraController.Instance;
        hp = GetComponent<Health>();
        fighter = GetComponent<Fighter>();

        isEnglish = IsEnglish();
        TriggerBoss();
    }

    private void ActivateAllShields() 
    {
        
        shieldList.ForEach(shield => { shield.ActivateInvincibility(); });
        shieldsParent.DOLocalMoveY(-10f, 1f);
    }
    private void DeactivateAllShields() {shieldList.ForEach(shield => { shield.DeactivateInvincibility(); }); shieldsParent.DOLocalMoveY(-1.8f, 2f).SetEase(Ease.InBounce); }

    public void OnShieldDestroyed()
    {
        //Debugger.Instance.CreateWarningLog("Shield Destroyed!");
        //dialogueSystem.CreateDialogue(new string[1] { "STOP IT!!!" }, witchFace, 20f);
        hp.TakeDamage(20, false);

        shieldList.RemoveAt(0);

        if (shieldList.Count == 0)
        {
            hp.TakeDamage(400, false);
        }
    }
    public void TriggerBoss()
    {
        if (!active) { ActivateAllShields(); StartCoroutine("BossBehaviour"); }
    }

    private bool IsEnglish() => SaveGame.Load<string>("Language") == "English";
    private Vector3 GetTargetPosition() => player.transform.position;

    private Vector3 GetRandomNearPosition(Vector3 center, float radius)
    {
        float randomX = Random.Range(-radius, radius);
        float randomZ = Random.Range(-radius, radius);

        return center + new Vector3(randomX, 0f, randomZ);
    }
    private IEnumerator BossBehaviour()
    {
        int areaDmgCounter = 0;

        Debug.Log("Current state: " + state);
        //ActivateAllShields();

        while (state == 0)
        {
            // ++++ Phase 1 +++++
            // WHILE HP > 180f
            // casts normal spells

            active = true;

            Vector3 targetPos = GetTargetPosition();

            model.LookAt(targetPos);

            animator.Play("Dance");

            fighter.enabled = false;

            if (hp._curHP < hp._HP)
            {
                fighter.enabled = true;
            }

            if (hp._HP - hp._curHP >= 60f)
            {
                Debug.Log("Witch State 2");
                //CreateWarningLog("Witch - State 2 start!"); 
                //dialogueSystem.CreateDialogue(new string[1] { "ARRGHHH!!!" }, witchFace, 20f);

                //if (isEnglish) dialogueSystem.CreateDialogue(new string[2] { $"ARGHH!%!&%#!", "YOU WILL REGRET THIS" }, witchFace, 40f);
                //else dialogueSystem.CreateDialogue(new string[2] { $"ARGHH!%!&%#!", "DU WIRST DAS BEREUEN" }, witchFace, 40f);
                fighter.enabled = true;

                state = 1;
            }

            yield return new WaitForSeconds(2f);
        }

        while (state == 1)
        {

            // ++++ Phase 2 +++++
            // WHILE HP > 120
            // the witch gets angry and casts area dmg spells.

            Debug.Log("State 2, area dmg counter: " + areaDmgCounter);

            active = true;

            areaDmgCounter++;
            animator.Play("Attack 1");

            Vector3 targetPos = GetTargetPosition();

            model.LookAt(targetPos);
            cam.ShakeCamera(10f);
            Debug.Log("Shake camera");

            // Spawn Area DMG FX
            if (areaDamageFX != null) Instantiate(areaDamageFX, GetRandomNearPosition(targetPos, 3f), Quaternion.identity);
            Debug.Log("spawn area dmg");

            if (hp._HP - hp._curHP >= 120f)
            {

                Debug.Log("Witch State 3");
                if (isEnglish) dialogueSystem.CreateDialogue(new string[1] { "The witch is stunned! The shields!! NOW!" }, witchFace, 30f);
                else dialogueSystem.CreateDialogue(new string[1] { "Die Hexe kann sich nicht bewegen! Die Schilder!! Jetzt!" }, witchFace, 30f);

                fighter.enabled = false;

                BossManager.Instance.DeactivateSpawning();

                animator.StopPlayback();

                DeactivateAllShields();

                state = 2;
            }

            yield return new WaitForSeconds(2f);

        }

        while (state == 2)
        {
            // ++++ Phase 3 +++++
            // WHILE HP > 60
            // the witch is stunned
            // destroy the shields to deal damage

            active = true;

            //Instantiate(stunnedFX, transform.position, Quaternion.identity);
            Instantiate(stunnedFX, model.transform.position, Quaternion.identity);

            //fighter.enabled = false;

            //BossManager.Instance.DeactivateSpawning();

            //animator.StopPlayback();

            //DeactivateAllShields();

            // TODO: Activate Shield can get damage

            BossManager.Instance.DeactivateSpawning();


            yield return new WaitForSeconds(1.5f);


            if (hp._curHP <= 30f)
            {
                //! Start state 4

                fighter.enabled = true;

                //BossManager.Instance.ActivateSpawning();
                animator.StartPlayback();
                //ActivateAllShields();
                if (isEnglish) dialogueSystem.CreateDialogue(new string[1] { $"NOOO...." }, witchFace, 20f);
                else dialogueSystem.CreateDialogue(new string[1] { $"NEEINN...." }, witchFace, 20f);

                state = 3;
            }
        }


        while (state == 3)
        {

            // ++++ Phase 4 (End) +++++
            // WHILE HP > 0
            // the witch casts normal spells
            // destroy the shields to deal damage

            active = true;

            if (hp._curHP <= 0f)
            {
                // instantiate nuke
                if (nukeFX != null) { Instantiate(nukeFX, model.transform.position, Quaternion.identity); }
                if (isEnglish) dialogueSystem.CreateDialogue(new string[1] { "NNNOOOOO!!!" }, witchFace, 20f);
                else dialogueSystem.CreateDialogue(new string[1] { "NNEEEINNN!!!" }, witchFace, 20f);
                state = 4;
            }
            state = 4;
        }

        BossManager.Instance.ActivateSpawning();


        animator.Play("Dance");
        active = false;
        yield return null;
    }
}
