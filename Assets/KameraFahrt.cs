using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class KameraFahrt : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;      // Deine Hauptkamera
    public CinemachineVirtualCamera triggeredCamera; // Die Kamera, zu der du wechseln m�chtest
    private bool isInTrigger = false;
    bool hadbeentriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // �berpr�fen, ob das GameObject, das den Trigger betreten hat, das gew�nschte ist (z.B. der Spieler)
        if (other.CompareTag("Player") && !isInTrigger && !hadbeentriggered)
        {
            isInTrigger = true;

            // Wechsle zur 'triggeredCamera'
            mainCamera.Priority = 0;
            triggeredCamera.Priority = 10;

            // Starte den Timer
            StartCoroutine(SwitchBackToMainCamera());
        }
    }

    private IEnumerator SwitchBackToMainCamera()
    {
        yield return new WaitForSeconds(4.5f); // Warte 4,5 Sekunden

        // Wechsle zur�ck zur Hauptkamera
        triggeredCamera.Priority = 0;
        mainCamera.Priority = 10;
        hadbeentriggered = true;
        isInTrigger = false;
    }
}
