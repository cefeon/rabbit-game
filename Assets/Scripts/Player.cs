using System;
using Unity.Netcode;
using UnityEngine;

public class Player : MonoBehaviour
{
        public float Stamina { get; set; } = 8;
        private float _maxStamina;
        public float StaminaRegeneration { get; set; } = 0.01f;
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

        public Player getLocalPlayer()
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

        private void Start()
        {
                _maxStamina = Stamina;
        }
        
        private void Update()
        {
                if (Stamina <= _maxStamina)
                {
                        Stamina += StaminaRegeneration;
                }
        }
}
