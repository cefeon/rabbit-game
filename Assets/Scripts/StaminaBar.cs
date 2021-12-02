using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = player.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.getLocalPlayer().Stamina;
    }
}
