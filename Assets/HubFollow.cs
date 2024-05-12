using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubFollow : MonoBehaviour
{
    // Start is called before the first frame update


    public Transform Player;
    public float yOffset;
    public float zOffset;

    
    // Update is called once per frame
    void Update()
    {

        if (Player != null)
        {
        //    transform.position = new Vector3(Player.position.x, Player.position.y + yOffset, transform.position.z);
          //  transform.position = new Vector3(Player.position.x, Player.position.y + yOffset, Player.position.z + zOffset);
            transform.position = new Vector3(Player.position.x, 4.074f, Player.position.z + zOffset);
        }

        


    }
    
}
