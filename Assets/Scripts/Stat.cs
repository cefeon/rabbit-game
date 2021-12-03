using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    public float value;
    [SerializeField]
    public float maxValue;
    [SerializeField]
    public float regenerateAmount;
    
    public void Regenerate()
    {
        if (value < maxValue - regenerateAmount){
            value += regenerateAmount;
        }
    }

    public void add(float amount)
    {
        value += amount;
    }
}