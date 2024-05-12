using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform _camtarget;
    [SerializeField] Animator CamAnim1, CamAnim2;
    bool _isLeft = false;

    private void Start()
    {
        _camtarget = GameObject.Find("CamPivot").transform;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!_isLeft)
            {
                CamAnim2.SetBool("isLeft", true);
                CamAnim1.SetBool("isLeft", true);
                   _isLeft = true;
            }
            else
            {
                CamAnim2.SetBool("isLeft", false);
                CamAnim1.SetBool("isLeft", false);
                _isLeft = false;
            }
        }
    }

    void LateUpdate()
    {
        transform.position = _camtarget.position;
    }
}
