using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
    [SerializeField] Health SkellHealth;
    [SerializeField] Health targetBoss;
    [SerializeField] Transform deathPositon;
    [SerializeField] Projectile projectile;
    [SerializeField] int weaponDamage;
    private bool shot;
    private BossManager bossManager;
    // Update is called once per frame
    private void Start()
    {
        bossManager = GameObject.Find("BossRoomManager").GetComponent<BossManager>();
        targetBoss = GameObject.Find("Witch").GetComponent<Health>();
    }
    void Update()
    {
        if (SkellHealth.isDead && !shot)
        {
            bossManager.enemylist.Remove(SkellHealth.gameObject);
            LaunchProjectile(deathPositon, targetBoss);
            shot = true;
        }
    }


    public void LaunchProjectile(Transform Shootpoint, Health target)
    {
        
        Projectile bullet = Instantiate(projectile, Shootpoint.position, Quaternion.identity);
        bullet.SetTarget(target, weaponDamage);

    }
}
