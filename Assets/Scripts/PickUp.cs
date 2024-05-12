using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Weapon _weapon = null;
    [SerializeField] GameObject _PickUpFX;
    [SerializeField] GameObject _marker;
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //GetComponent<SaveWeapon>().SendInfoToSaveManager(_weapon);

            if(_weapon.name == "Sword 1")
            {
                SaveGame.Save<string>("Weapon", "BasicSword");
                if(!SaveGame.Exists("BasicSword"))
                SaveGame.Save<int>("BasicSword", 1);

            }
            if (_weapon.name == "Sword 2")
            {
                SaveGame.Save<string>("Weapon", "KnightSword");
                if (!SaveGame.Exists("KnightSword"))
                SaveGame.Save<int>("KnightSword", 1);
            }     
            if(_weapon.name == "Sword 3")
            {
                SaveGame.Save<string>("Weapon", "HeroSword");
                if (!SaveGame.Exists("HeroSword"))
                SaveGame.Save<int>("HeroSword", 1);
            }



            other.GetComponent<Fighter>().EquipWeapon(_weapon);
            Instantiate(_PickUpFX, transform.position, Quaternion.identity);
            Destroy(_marker);
            Destroy(gameObject, 0.01f);
        }
    }


}
