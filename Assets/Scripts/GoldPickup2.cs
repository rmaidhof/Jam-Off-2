using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GoldPickup2 : PlayerPickup
{
    public int value;
    private GameManager gameManager; 

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void PickupBonus()
    {
        gameManager.AddScore(value);
    }

}
