using UnityEngine;

public class SaveWeapon : MonoBehaviour
{
    public void SendInfoToSaveManager(Weapon weapon)
    {
        SaveManager.instance.WeaponSave = weapon;
    }
}
