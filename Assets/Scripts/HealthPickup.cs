using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PlayerPickup
{
    public int value;
    private HealthManager healthManager;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    public override void PickupBonus()
    {
        healthManager.HealPlayer(value);
    }
}
