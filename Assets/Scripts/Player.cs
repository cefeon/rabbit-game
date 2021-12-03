using System;
using Unity.Netcode;
using Unity.Netcode.Samples;
using UnityEngine;
using Object = System.Object;

public class Player : NetworkBehaviour, IAttacker
{
        [SerializeField]
        private Stat stamina;
        
        public float Mana { get; set; } = 10;
        public float ManaRegeneration { get; set; } = 0.01f;
        
        public float MovementSpeed { get; set; } = 10;
        
        public float Health { get; set; } = 100;
        
        public float AbilityPower { get; set; } = 10;
        public float AbilityHaste { get; set; } = 10;
        public float MagicPenetration { get; set; } = 10;
        
        public float AttackDamage { get; set; } = 10;
        public float AttackSpeed { get; set; } = 10;
        public float ArmorPenetration { get; set; } = 10;
        public float CritChance {get; set; } = 10;
        public float CritDamage {get; set; } = 10;
        
        public float LifeSteal {get; set; } = 10;

        public Weapon weaponLeftButton;

        public Player getLocalPlayerFromAnotherObject()
        {
                var playerObjects = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in playerObjects)
                {
                        if (player.GetComponent<NetworkObject>().IsLocalPlayer)
                        {
                                return player.GetComponent<Player>();
                        }
                }
                return null;
        }

        private void Update()
        {
                stamina.Regenerate();
        }

        public void Attack(Weapon weapon)
        {
                weapon.Shoot();
        }

        public Stat Stamina
        {
                get => stamina;
                set => stamina = value;
        }
}
