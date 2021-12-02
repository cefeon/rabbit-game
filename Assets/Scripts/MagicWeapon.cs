using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class MagicWeapon : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject IceBoltPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();            
        }
    }

    void Shoot()
    {
        Instantiate(IceBoltPrefab, firePoint.position, firePoint.rotation);
    }
}
