using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("Level 5")]
    [SerializeField] private GameObject _lvl5object; // planned
 
    private Health _healthComp;
    private Fighter _fighterComp;
    private Enemy _enemyComp;
    private Player _player;
    private Movement _movementComp;
    private Animator _animator;

    private bool _triggered;
    private bool _isEnglish;

    private int state = 0;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            _triggered = false;
        }
    }


    private IEnumerator BossBehaviour()
    {

        Debug.Log("coroutine start");

        while (state == 0)
        {
            if (_healthComp._curHP == _maxHP)
            {
                Debug.Log(" State 0 - Blocking.");
                _animator.Play("BlockPose");
                yield return new WaitForSeconds(_animDelay);
            }
            else state = 1;
        }

        while (state == 1)
        {
            //Debug.Log("Boss HP: " + _healthComp._curHP);
            Debug.Log(" State 1");

            if (_healthComp._curHP < _maxHP - 5)
            {
                // Activate TMP
                if (_showCrit) 
                {
                    _critTMP.SetActive(true);

                    // Reset scale
                    _critTMP.transform.localScale = Vector3.zero;

                    // Sclae up
                    _critTMP.transform.DOScale(Vector3.one + Vector3.one, _animSpd).SetEase(_ease);
                }

                // Jump TMP
                //_critTMP.transform.DOLocalJump(_critTMP.transform.localPosition, 1f, 1, _animSpd);

                // Jump KNIGHT 
                transform.DOLocalJump(transform.position, 1f, 2, _animSpd / 2f);

                // Delay
                yield return new WaitForSeconds(_animDelay);

                // Reset scale
                if (_showCrit) _critTMP.transform.DOScale(Vector3.zero, _animSpd).SetEase(_ease);

                // Trigger Dialogue
                TriggerNextDialogue(1);

                // Delay
                yield return new WaitForSeconds(_animDelay);

                // Reset Object
                if (_showCrit) _critTMP.gameObject.SetActive(false);

                // Set State
                state = 2;
            }
            
            yield return new WaitForSeconds(_animDelay);
        }

        while (state == 2)
        {
            Debug.Log("State 2");

            _movementComp.StartMoveAction(_destinations[0].position, 1);

            yield return new WaitForSeconds(_animDelay);

            WaveController.Instance.StartWave();

            state = 3;
            yield return new WaitForSeconds(20);
        }

        while (state == 3)
        {
            //transform.LookAt(_player.transform);
            Debug.Log("State 3");
            transform.LookAt(_player.transform);
            //WaveController.Instance.StartWave();
            state = 4;
            yield return new WaitForSeconds(20);
        }

        while (state == 4)
        {
            Debug.Log("State 4 and triggered!!!");

            _animator.Play("BlockPose");

            transform.LookAt(_player.transform);

            if (_lvl5object != null) { _lvl5object.SetActive(true); }

            if (_triggered) 
            { 
                TriggerNextDialogue(2);
                state = 5;
            }

            yield return new WaitForSeconds(_animDelay);
        }

        if (state == 5)
        {
            _movementComp.StartMoveAction(_destinations[1].position, 1);
            Debug.Log("Trigger start wave state 5");
            WaveController.Instance.StartWave();
            state = 6;
            yield return null;
        }

        if (state == 6)
        {
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