using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        //if (dialogueTrigger == null) { CreateWarningLog("No Dialogue Trigger found"); return; }
        //if (hp == null) { CreateWarningLog("HP not found"); return; }
        //if (fighter == null) { CreateWarningLog("Fighter not found"); return; }
        //if (cam == null) { CreateWarningLog("Camera Controller not found"); return; }
        //if (animator == null) { CreateWarningLog("Animator not found"); return; }
        //if (player == null) { CreateWarningLog("Player not found"); return; }
        //if (model == null) { CreateWarningLog("Model not found"); return; }
    }

    private void ActivateAllShields() => shieldList.ForEach(shield => { shield.ActivateInvincibility(); });
    private void DeactivateAllShields() => shieldList.ForEach(shield => { shield.DeactivateInvincibility(); });

    public void OnShieldDestroyed()
    {
        //Debugger.Instance.CreateWarningLog("Shield Destroyed!");
        //dialogueSystem.CreateDialogue(new string[1] { "STOP IT!!!" }, witchFace, 20f);
        hp.TakeDamage(20, false);
    }
    public void TriggerBoss()
    {
        if (!active) { ActivateAllShields(); StartCoroutine("BossBehaviour"); }
    }
    private Vector3 GetTargetPosition() => player.transform.position;
    private IEnumerator BossBehaviour()
    {
        int areaDmgCounter = 0;

        ActivateAllShields();

        while (state == 0)
        {
            // ++++ Phase 1 +++++
            // WHILE HP > 180f
            // casts normal spells

            active = true;

            Vector3 targetPos = GetTargetPosition();

            model.LookAt(targetPos);

            animator.Play("Dance");


            if (hp._curHP <= 180f)
            {
                //CreateWarningLog("Witch - State 2 start!");
                //dialogueSystem.CreateDialogue(new string[1] { "ARRGHHH!!!" }, witchFace, 20f);
                dialogueSystem.CreateDialogue(new string[3] { $"ARGHH!%!&%#!", "LORTNOC YM... NI EB AERA EHT TEL...", "YOU WILL REGRET THIS" }, witchFace, 40f);

                state = 1;
                break;
            }

            yield return new WaitForSeconds(2f);
        }

        while (state == 1)
        {

            // ++++ Phase 2 +++++
            // WHILE HP > 120
            // the witch gets angry and casts area dmg spells.

            active = true;

            areaDmgCounter++;
            animator.Play("Attack 1");

            Vector3 targetPos = GetTargetPosition();

            model.LookAt(targetPos);

            yield return new WaitForSeconds(.5f);
            model.LookAt(targetPos);
            cam.ShakeCamera(10f);

            // Spawn Area DMG FX
            if (areaDamageFX != null) Instantiate(areaDamageFX, targetPos, Quaternion.identity);

            yield return new WaitForSeconds(1.5f);

            model.LookAt(targetPos);

            if (hp._curHP <= 140f)
            {

                dialogueSystem.CreateDialogue(new string[1] { "The witch is stunned! The shields!! NOW!" }, witchFace, 30f);

                fighter.enabled = false;

                BossManager.Instance.DeactivateSpawning();

                animator.StopPlayback();

                DeactivateAllShields();

                state = 2;
                break;
            }
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


            if (hp._curHP <= 60f)
            {
                //! Start state 4

                fighter.enabled = true;

                //BossManager.Instance.ActivateSpawning();
                animator.StartPlayback();
                //ActivateAllShields();
                dialogueSystem.CreateDialogue(new string[1] { $"NOOO...." }, witchFace, 20f);


                state = 3;
                break;
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
                dialogueSystem.CreateDialogue(
                new string[1] { "NNNOOOOO!!!" },
                witchFace,
                20f);
                state = 4;
                break;
            }
            state = 4;
            break;
        }

        animator.Play("Dance");
        active = false;
        yield return null;
    }
}
