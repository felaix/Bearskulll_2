using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(float power = 5f)
    {
        Debug.Log("Shaking cam");
        cinemachineImpulseSource.GenerateImpulse(power);
    }

}
