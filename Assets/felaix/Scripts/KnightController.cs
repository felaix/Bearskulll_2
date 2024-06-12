using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField][Range(100f, 300f)] private int _maxHP = 300;

    [Header("Destinations")]
    [SerializeField] private List<Transform> _destinations;

    [Header("UI")]
    [SerializeField] private GameObject _critTMP;

    [SerializeField] private GameObject _enemyPrefab;

    private Health _healthComp;
    private Fighter _fighterComp;
    private Enemy _enemyComp;
    private Player _player;
    private Movement _movementComp;
    private Animator _animator;

    private bool _triggered;
    private bool _isEnglish;

    [SerializeField] private List<DialogueMultiLanguage> _dialogues = new();

    private void Start()
    {
        _healthComp = GetComponent<Health>();
        _fighterComp = GetComponent<Fighter>();
        _enemyComp = GetComponent<Enemy>();
        _movementComp = GetComponent<Movement>();
        _animator = _movementComp._anim;

        _healthComp._HP = (int)_maxHP;
        _healthComp._curHP = (int)_maxHP;
        _enemyComp.enabled = false;
        _isEnglish = SaveGame.Load<string>("Language") == "English";

        Invoke("InvokeBlockPose", .25f);
    }

    private void InvokeBlockPose()
    {
        _animator.Play("BlockPose");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;

        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            _triggered = true;
            StartCoroutine(BossBehaviour());
        }
    }


    private IEnumerator BossBehaviour()
    {
        TriggerNextDialogue(0);

        int state = 0;

        while (state == 0)
        {
            if (_healthComp._curHP == _maxHP)
            {
                _animator.Play("BlockPose");
                yield return new WaitForSeconds(.1f);
            }
            else state = 1;
        }

        while (state == 1)
        {
            Debug.Log("Boss HP: " + _healthComp._curHP);
            if (_healthComp._curHP < _maxHP - 10)
            {

                _critTMP.SetActive(true);
                _critTMP.transform.localScale = Vector3.zero;
                _critTMP.transform.DOScale(Vector3.one + Vector3.one, 1f).SetEase(Ease.InOutBounce);
                _critTMP.transform.DOLocalJump(_critTMP.transform.localPosition, 1f, 1, 1f);
                transform.DOLocalJump(transform.position, 1f, 2, .5f);
                yield return new WaitForSeconds(.5f);
                _critTMP.transform.DOScale(Vector3.zero, 1f);
                TriggerNextDialogue(1);
                yield return new WaitForSeconds(.5f);
                _critTMP.gameObject.SetActive(false);
                state = 2;
            }

            yield return new WaitForSeconds(.5f);

        }

        while (state == 2)
        {
            _movementComp.StartMoveAction(_destinations[0].position, 1);
            state = 3; break;
        }

        while (state == 3)
        {
            //transform.LookAt(_player.transform);
            WaveController.Instance.StartWave();
            yield return new WaitForSeconds(1f);



        }

        yield return null;
    }

    private void TriggerNextDialogue(int index)
    {
        if (_isEnglish)
        {
            DialogBoxText.instance.SetDialog(_dialogues[index].DialogueEnglish, null, 20f);
        }
        else
        {
            DialogBoxText.instance.SetDialog(_dialogues[index].DialogueGerman, null, 20f);
        }

        DialogBoxText.instance.TriggerDialog();
    }


}

[System.Serializable]
public struct DialogueMultiLanguage
{
    public string[] DialogueEnglish;
    public string[] DialogueGerman;
}