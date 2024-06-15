using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Patrol _patrolPath;
    [SerializeField] float _chaseDistance = 5;
    [SerializeField] GameObject player;
    private Vector3 startPosition;

    [SerializeField] private float _lastTimeSawPlayer;
    [SerializeField] private float _lostSightTime = 3;
    private float _searchTime;
    [SerializeField] private float _gotToWaypointTime;
    [SerializeField] private bool _randomWait;
    [SerializeField] private float _waypointWaitTime = 3;
    private float _waitTime;
    [SerializeField] private float _waypointTolerance = 0.5f;
    [SerializeField] private int _waypointID = 0;

    [Range(0, 1)][SerializeField] float patrolSpeedFraction = 0.5f;
    bool SoulAdded = false;
    bool dropped = false;
    bool died = false;
    [SerializeField] GameObject itemDrop;

    public bool CanAttack = true;

    private void Start()
    {
        startPosition = transform.position;
        _waitTime = _waypointWaitTime;
        _searchTime = _lostSightTime;

        if (EnemyCounter.Instance != null) EnemyCounter.Instance.AddEnemy(this);
    }

    private void Update()
    {


        if (GetComponent<Health>().isDead)
        {
            //if (EnemyCounter.Instance != null) EnemyCounter.Instance.RemoveEnemy(this);
            Die();
            AddSoul();
            ItemDrop();
            GetComponent<Fighter>().Cancel();
            GetComponent<Movement>().Cancel();
        }
        else
        {
            if (isChasing())
            {
                if (CanAttack) GetComponent<Fighter>().Attack(player);
                _lastTimeSawPlayer = Time.time;
            }
            else if (Time.time - _lastTimeSawPlayer <= _searchTime)
            {
                GetComponent<Movement>().Cancel();
                GetComponent<Fighter>().Cancel();
            }
            else if (Time.time - _gotToWaypointTime <= _waitTime)
            {
                GetComponent<Movement>().Cancel();
                GetComponent<Fighter>().Cancel();
            }
            else if (Time.time - _lastTimeSawPlayer > _lostSightTime)
            {
                PatrolBehavior();
            }
        }

    }

    private void Die()
    {
        if (died) return;
        died = true;
        if (EnemyCounter.Instance != null) EnemyCounter.Instance.RemoveEnemy(this);
    }

    public void ItemDrop()
    {

        if (!dropped && itemDrop != null)
        {
            Instantiate(itemDrop, transform.position + Vector3.up, Quaternion.identity);
            dropped = true; 
        }
    }

    private void AddSoul()
    {
        
        if (!SoulAdded)
        {
            SaveManager.instance.addSoul();
            SoulAdded = true;
        }
    }
    private void PatrolBehavior()
    {

        Vector3 nextPosition = startPosition;

        if (_patrolPath != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();


                if (_randomWait)
                {
                    _waitTime = Random.Range(0, _waypointWaitTime);
                    _lostSightTime = Random.Range(0, _lostSightTime);
                }
                _gotToWaypointTime = Time.time;
            }
            nextPosition = GetCurrentWaypoint();
        }
        GetComponent<Movement>().StartMoveAction(nextPosition, patrolSpeedFraction);


    }

    private bool AtWaypoint()
    {
        return (Vector3.Distance(transform.position, GetCurrentWaypoint()) < _waypointTolerance);

    }

    private void CycleWaypoint()
    {
        _waypointID = _patrolPath.getNextIndex(_waypointID);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return _patrolPath.GetWaypoint(_waypointID);
    }

    private bool isChasing()
    {
        player = GameObject.FindWithTag("Player");
        return Vector3.Distance(transform.position, player.transform.position) < _chaseDistance;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _chaseDistance);
    }
}
