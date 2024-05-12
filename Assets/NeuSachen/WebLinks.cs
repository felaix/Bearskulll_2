using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLinks : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject zumachen;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Discord()
    {
        Application.OpenURL("https://discord.gg/32k6HJu6Gk");
    }
    
    public void Discord2()
    {
        Application.OpenURL("https://discord.gg/AU6gXNDrxX");
    }

    public void Website()
    {
        Application.OpenURL("https://dream-studios.net/");
        
    }


    public void Speedrun( )
    {
        Application.OpenURL("https://store.steampowered.com/news/app/2368130/view/3866965184375393483");
    }


    public void kickstart()
    {
        Application.OpenURL("https://www.kickstarter.com/projects/dreamstudios/neospace");
    }

    public void Neospace()
    {
        Application.OpenURL("https://store.steampowered.com/app/2248400/Neospace/");
    }

    public void Ranchlife() {


        Application.OpenURL("https://store.steampowered.com/app/1882460/Ranchlife/");



    }


    public void Steam()
    {
                Application.OpenURL("https://store.steampowered.com/publisher/Dreamstudio");
    }

    public void Spaceshipracer()
    {

        Application.OpenURL("https://store.steampowered.com/app/2360070/Spaceship_Racer_Portal");

    }


    public void close() {

        zumachen.SetActive(false);


    }




}
