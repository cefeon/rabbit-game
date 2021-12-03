using Unity.Netcode.Samples;
using UnityEngine;

public class MagicWeapon : Weapon
{
    public Transform firePoint;
    public GameObject IceBoltPrefab;
    
    public override void Shoot()
    {
        Instantiate(IceBoltPrefab, firePoint.position, firePoint.rotation);
    }
}
