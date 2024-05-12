using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWeapon : MonoBehaviour
{
    
    public void SendInfoToSaveManager(Weapon weapon)
    {
        SaveManager.instance.WeaponSave = weapon;
    }
}
