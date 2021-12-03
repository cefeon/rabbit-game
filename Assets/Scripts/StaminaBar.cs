using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public Player player;

    void Start()
    {
        slider.maxValue = player.getLocalPlayerFromAnotherObject().Stamina.maxValue;
    }
    
    void Update()
    {
        slider.value = player.getLocalPlayerFromAnotherObject().Stamina.value;
    }
}
