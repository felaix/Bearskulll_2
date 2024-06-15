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
    [SerializeField] private bool _showCrit; // can be deleted later

    [Header("Animations")]
    [SerializeField][Range(0.1f, 10f)] private float _animSpd = 1f;
    [SerializeField][Range(0.1f, 10f)] private float _animDelay = .5f;
    [SerializeField] private Ease _ease;
    //[SerializeField] private GameObject _enemyPrefab;

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
                yield return new WaitForSeconds(_animDelay);
            }
            else state = 1;
        }

        while (state == 1)
        {
            Debug.Log("Boss HP: " + _healthComp._curHP);
            if (_healthComp._curHP < _maxHP - 5)
            {
                // Activate Object
                if (_showCrit) _critTMP.SetActive(true);

                // Reset scale
                _critTMP.transform.localScale = Vector3.zero;

                // Sclae up
                _critTMP.transform.DOScale(Vector3.one + Vector3.one, _animSpd).SetEase(_ease);

                // Jump TMP
                //_critTMP.transform.DOLocalJump(_critTMP.transform.localPosition, 1f, 1, _animSpd);

                // Jump KNIGHT 
                transform.DOLocalJump(transform.position, 1f, 2, _animSpd / 2f);

                // Delay
                yield return new WaitForSeconds(_animDelay);

                // Reset scale
                _critTMP.transform.DOScale(Vector3.zero, _animSpd).SetEase(_ease);

                // Trigger Dialogue
                TriggerNextDialogue(1);

                // Delay
                yield return new WaitForSeconds(_animDelay);

                // Reset Object
                _critTMP.gameObject.SetActive(false);

                // Set State
                state = 2;
            }
        }

        while (state == 2)
        {
            _movementComp.StartMoveAction(_destinations[0].position, 1);
            state = 3;
        }

        while (state == 3)
        {
            //transform.LookAt(_player.transform);
            Debug.Log("knight state 3");
            WaveController.Instance.StartWave();
            state = 4;
            yield return new WaitForSeconds(1f);
        }

        if (state == 4)
        {
            Debug.Log("knight state 4");

            yield return null;
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