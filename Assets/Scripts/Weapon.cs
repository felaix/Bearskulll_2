using System;
using UnityEngine;


    [CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon", order = 0)]
    public class Weapon : ScriptableObject
{
    public GameObject weaponPref = null;
    public float weaponSpeed;
    public int weaponDamage;
    public float weaponRange     = 2;
    public bool leftHanded;

    public bool isRangeWeapon;
    public Projectile projectile = null;

    const string weaponName = "Weapon";

    public void Spawn(Transform HandTransformR, Transform HandTransformL)
    {
        DestroyOldWeapon(HandTransformR, HandTransformL);
        Transform HandTrans = GetTransform(HandTransformR, HandTransformL);
        GameObject weapon = Instantiate(weaponPref, HandTrans);
        weapon.name = weaponName;
    }

    public void LaunchProjectile(Transform HandTransformR, Transform HandTransformL, Health target)
    {
        Transform HandTrans = GetTransform(HandTransformR, HandTransformL);
        Projectile bullet = Instantiate(projectile, HandTrans.position, Quaternion.identity);
        bullet.SetTarget(target, weaponDamage);

    }

    private Transform GetTransform(Transform handTransformR, Transform handTransformL)
    {
        Transform handTransform;
        if(!leftHanded) handTransform = handTransformR;
        else handTransform = handTransformL;
        return handTransform;
    }

    private void DestroyOldWeapon(Transform handTransformR, Transform handTransformL)
        {
        Transform OldWeapon = handTransformR.Find(weaponName);
        if(OldWeapon == null)
        { 
            OldWeapon = handTransformL.Find(weaponName); 
        }
        if (OldWeapon == null) return;

        OldWeapon.name = "DestroyedWeapon";
        Destroy(OldWeapon.gameObject);
    }


}



