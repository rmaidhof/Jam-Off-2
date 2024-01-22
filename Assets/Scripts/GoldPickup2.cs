using System;
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
        GameManager.onNegativeCoins += TurnNegative;
        GameManager.onPositiveCoins += TurnPositive;

    }

    private void TurnNegative()
    {
        value = -Mathf.Abs(value);
    }

    private void TurnPositive()
    {
        value = Mathf.Abs(value);
    }

    public override void PickupBonus()
    {
        gameManager.AddScore(value);
    }

}
