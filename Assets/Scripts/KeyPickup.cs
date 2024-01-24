using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class KeyPickup : PlayerPickup
{
    public bool lowGravity;
    public bool tallEnemies;
    public bool negativeCoins;
    public TextMeshProUGUI keyAcquiredText;
    public TextMeshProUGUI ruleText;
    public PlayerInput playerInput;
    public HealthManager healthManager;

    public GameManager gameManager;

    public bool blueKey = false;
    public bool redKey = false;
    public bool greenKey = false;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        playerInput = FindObjectOfType<PlayerInput>();
        healthManager = FindObjectOfType<HealthManager>();
        StartCoroutine(findGM());

    }

    public override void PickupBonus()
    {
        gameManager.LowGravity = lowGravity;
        gameManager.TallEnemies = tallEnemies;
        gameManager.NegativeCoins = negativeCoins;

        if (blueKey)
        {
            gameManager.blueKeyAcquired = true;
        }
        else if (redKey)
        {
            gameManager.redKeyAcquired = true;
        }
        else if (greenKey)
        {
            gameManager.greenKeyAcquired = true;
        }


        keyAcquiredText.enabled = true;

        ruleText.enabled = true;
        playerInput.enabled = false;
        healthManager.invincibilityCounter = 10;
    }

    IEnumerator findGM()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = FindObjectOfType<GameManager>();
        yield return null;
    }
}
