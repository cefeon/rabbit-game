using System;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;

public class Enemy : NetworkBehaviour, IAttacker
{
    public float MovementSpeed = 10;
    public Stat Health;
    public Weapon Weapon;

    public void Start()
    {
        Health.value = Health.maxValue;
    }

    public void Attack(Weapon weapon)
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        if (Health.value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
