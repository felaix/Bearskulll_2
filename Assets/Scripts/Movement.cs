using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour, IAction
{

    [SerializeField] Animator _anim;
    public float maxSpeed;
    NavMeshAgent navMeshAgent;
    public bool isKnockback;
    public bool isPlayer;
    public bool IsWasd = false;
    bool WASDACTIVATED = false;
    public Rigidbody PlayerRB;


    float moveHorizontal = 0;
    float moveVertical = 0;



    public bool InvertMainMap = false;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        maxSpeed = navMeshAgent.speed;
        WASDACTIVATED = SaveGame.Load<bool>("WASDACTIVATED");
    }

    void Update()
    {
        UpdateAnimator();
        if(isPlayer)
        {
            if (WASDACTIVATED)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    IsWasd = true;
                    GetComponent<ActionState>().StartAction(this);

                    if (InvertMainMap == false)
                    {
                        moveHorizontal = Input.GetAxis("Horizontal");
                        moveVertical = Input.GetAxis("Vertical");
                    }
                    else
                    {
                        moveHorizontal = Input.GetAxis("Vertical");
                        moveVertical = -Input.GetAxis("Horizontal");
                    }



                    // Bewegungsrichtung festlegen, die Achsen sind umgepolt
                    Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);

                    // Charakter in die Bewegungsrichtung drehen
                    if (movement != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(movement);
                    }

                    // Position direkt setzen
                    transform.position += movement * maxSpeed * Time.deltaTime;
                    navMeshAgent.isStopped = true;

                }

                else
                {
                    if (IsWasd)
                    {
                        IsWasd = false;

                        _anim.SetFloat("movespeed", 0);
                    }
                }


            }
            
        }
    }

    private void FixedUpdate()
    {
        if (isKnockback)
            navMeshAgent.velocity = -Vector3.forward;
    }

    public void Cancel()
    {
        navMeshAgent.isStopped = true;

    }
    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
        GetComponent<ActionState>().StartAction(this);
        MoveTo(destination, speedFraction);
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped = false;
    }

    void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        if (!IsWasd)
        {
            _anim.SetFloat("movespeed", speed);
        }
        if (IsWasd) 
        { 
            _anim.SetFloat("movespeed", 1);
        }

    }

}

