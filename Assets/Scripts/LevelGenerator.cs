using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            //pixel is transparent
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.Color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.Prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
