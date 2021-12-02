using System;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab
{
        [SerializeField]
        private Color color;
        [SerializeField]
        private GameObject prefab;

        public Color Color
        {
                get => color;
                set => color = value;
        }

        public GameObject Prefab {
                get => prefab;
                set => prefab = value;
        }
}