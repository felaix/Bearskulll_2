using Steamworks;
using UnityEngine;

public class DlcCheck : MonoBehaviour
{
    // Replace with your DLC's App ID
    public AppId_t dlcAppId = new AppId_t(2410830); // 2410830
    public GameObject KleinerHelferlein;
    public Material GoldMaterial;

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steamworks is not initialized!");
            return;
        }

        if (SteamApps.BIsSubscribedApp(dlcAppId))
        {
            Debug.Log("DLC is installed!");
            KleinerHelferlein.GetComponent<Renderer>().material = GoldMaterial;
        }
        else
        {
            Debug.Log("DLC is not installed.");
        }
    }
}