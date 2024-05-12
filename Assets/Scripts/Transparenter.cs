using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparenter : MonoBehaviour
{
    
    private Material[] _InitialColor;
    private GameObject player;
    private GameObject cam;
    [SerializeField] private Material[] _transparent;
    [SerializeField] float _triggerrange = 4;
    [SerializeField] LayerMask _isPlayer;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        _InitialColor = GetComponent<Renderer>().materials;
    }


    void Update()
    {

        if (GetInRange())
        {
            GetComponent<Renderer>().materials = _transparent;
            
        }
        else
            GetComponent<Renderer>().materials = _InitialColor;

    }
    private bool GetInRange()
    {
        if (Vector3.Distance(cam.transform.position, player.transform.position) > Vector3.Distance(cam.transform.position, transform.position)
            && Vector3.Distance(transform.position, player.transform.position) < _triggerrange)
            return true;
        else
            return false;
    }

}
