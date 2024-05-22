using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using Steamworks;

public class BossManager : MonoBehaviour
{

    public static BossManager Instance;
    [Header("StartFight")]
    public bool PlayerIsInArena;
    public GameObject Player;
    public GameObject PlayerUI;

    [Header("Fight")]
    public List<GameObject> enemylist = new List<GameObject>();
    public List<GameObject> itemlist = new List<GameObject>();
    [SerializeField] Transform[] _enemyspawnpoints;
    [SerializeField] Transform[] _itemspawnpoints;
    [SerializeField] GameObject[] _enemys;
    [SerializeField] GameObject[] _items;
    bool _spawning;

    private SaveManager _saveManager;

    [Header("EndFight")]
    bool _ending;
    [SerializeField] AudioClip _clip;
    [SerializeField] Health bossHealth;
    [SerializeField] Animator _faderAnim;
    [SerializeField] Animator _faderAnimEN;
    [SerializeField] Text EndText;
    [SerializeField] Text EndTextEN;
    public GameObject BridgeStop;

    public BossController bossController;
    private void Awake()
    {
        Instance = this;
    }


    public void DeactivateSpawning() => _spawning = true;
    public void ActivateSpawning() => _spawning = false;

    private void OnTriggerEnter(Collider other)
    {
        BridgeStop.SetActive(true);

        _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        if (other.GetComponent<Player>() != null)
        {
            PlayerIsInArena = true;
            bossController.TriggerBoss();
        }
    }
    private void Update()
    {

        if (PlayerIsInArena && !bossHealth.isDead)
        {
            int rng = Random.Range(0, 5);

            if (enemylist.Count < 2 && !_spawning)
            {
                StartCoroutine(SpawnEnemy(_enemyspawnpoints[rng]));
            }
            if (itemlist.Count < 2 && !_spawning)
            {
                StartCoroutine(SpawnItem(_itemspawnpoints[rng]));
            }
        }

        if (bossHealth.isDead && !_ending)
        {
            _saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
            GameEnd();

        }
        if (_ending)
        {
            for (int i = 0; i < enemylist.Count; i++)
            {
                Destroy(enemylist[i]);
            }
        }
    }

    IEnumerator SpawnEnemy(Transform where)
    {
        _spawning = true;
        yield return new WaitForSeconds(3);
        GameObject spawn = Instantiate(_enemys[Random.Range(0, _enemys.Length)], where.position, Quaternion.identity);
        enemylist.Add(spawn);
        yield return new WaitForSeconds(2);
        _spawning = false;
    }

    IEnumerator SpawnItem(Transform where)
    {
        _spawning = true;
        yield return new WaitForSeconds(3);
        GameObject spawn = Instantiate(_items[Random.Range(0, _items.Length)], where.position, Quaternion.identity);
        itemlist.Add(spawn);
        yield return new WaitForSeconds(2);
        _spawning = false;
    }


    private void GameEnd()
    {
        _ending = true;
        for (int i = 0; i < enemylist.Count; i++)
        {
            Destroy(enemylist[i]);
        }
        if (SaveGame.Load<string>("Language") == "English")
        {



            int currentStatValue;
            bool gotStat = SteamUserStats.GetStat("SOULS", out currentStatValue);

            EndTextEN.text = "You have " + currentStatValue + " Souls freed.";
            Debug.Log("GameEnd");
            _faderAnimEN.SetTrigger("End");
            PlayerUI.SetActive(false);
            Player.GetComponent<Player>().enabled = false;
            Player.GetComponent<Health>()._curHP = 100000;
            AudioManager.instance.PlayMusic(_clip);
            _saveManager.ResetSave();







        }
        else
        {

            int currentStatValue;
            bool gotStat = SteamUserStats.GetStat("SOULS", out currentStatValue);

            EndText.text = "Du hast " + currentStatValue + " Seelen befreit.";
            Debug.Log("GameEnd");
            _faderAnim.SetTrigger("End");
            PlayerUI.SetActive(false);
            Player.GetComponent<Player>().enabled = false;
            Player.GetComponent<Health>()._curHP = 100000;
            AudioManager.instance.PlayMusic(_clip);
            _saveManager.ResetSave();
        }
    }




}
