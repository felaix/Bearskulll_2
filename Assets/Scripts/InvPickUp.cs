using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public enum item { Heal, Key, Energy };
public class InvPickUp : MonoBehaviour
{
    
    [SerializeField] GameObject _PickUpFX;
    public item item;
    private BossManager bossManager;
    InvFailPickUp invFailPickUp;
    int enegymax = 2;
    int healthmax = 4;



    private void Start()
    {
        invFailPickUp = FindObjectOfType<InvFailPickUp>();
        if (GameObject.Find("BossRoomManager") != null)
        {
            bossManager = GameObject.Find("BossRoomManager").GetComponent<BossManager>();
        }

        if(SaveGame.Exists("BigBag"))
        {
            enegymax = 3;
            healthmax = 5;
        }
        if (SaveGame.Exists("SuperBag"))
        {
            enegymax = 4;
            healthmax = 7;
        }

        
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inv = other.GetComponent<Inventory>();
            Instantiate(_PickUpFX, transform.position, Quaternion.identity);

            if (item == item.Heal)
            {
                if (inv.heart == healthmax)
                {
                    invFailPickUp.HealthFailPickUp();

                    return;
                }
                else
                    inv.addHeal();
            }

            if(item == item.Key)    inv.addKey();
            
            if (item==item.Energy)   
            {
                if (inv.energy == enegymax)
                {
                    //rot
                    invFailPickUp. EnergyFailPickUp();
                    return;
                }
                else
                    inv.addEnergy();
            }
            if(bossManager != null)
                bossManager.itemlist.Remove(gameObject);

            Destroy(gameObject);
        }
    }
    
}
