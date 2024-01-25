using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GoldPickup2 : PlayerPickup
{
    public int value;
    public GameManager gameManager; 
    public HealthManager healthManager;
    public GameObject positivePickupFX;
    public GameObject negativePickupFX;
    public Material positiveMat;
    public Material negativeMat;
    public MeshRenderer coinMesh;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.onNegativeCoins += TurnNegative;
        GameManager.onPositiveCoins += TurnPositive;
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(findGM());

    }
    private void Update()
    {

    }

    IEnumerator findGM()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = FindObjectOfType<GameManager>();
        yield return null;
    }

    private void TurnNegative()
    {
        value = -Mathf.Abs(value);
        pickupEffect = negativePickupFX;
        if (coinMesh != null)
        {
            coinMesh.material = negativeMat;
        }
    }

    private void TurnPositive()
    {
        value = Mathf.Abs(value);
        pickupEffect = positivePickupFX;
        if(coinMesh != null)
        {
            coinMesh.material = positiveMat;
        }
    }

    public override void PickupBonus()
    {
        gameManager.AddScore(value);
    }

}
