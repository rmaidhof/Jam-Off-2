using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;


public class UICanvas : MonoBehaviour
{
    public TextMeshProUGUI[] textsToDisable;

    public GameManager gameManager;

    public TextMeshProUGUI lowGravity;
    public TextMeshProUGUI tallEnemies;
    public TextMeshProUGUI negativeCoins;

    public TextMeshProUGUI keyAcquired;

    public float delay = 2f;
    
    public static UICanvas Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        gameManager = FindObjectOfType<GameManager>();
    }

    

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        foreach(TextMeshProUGUI text in textsToDisable)
        {
            text.enabled = false;
        }

        if (gameManager.LowGravity)
        {
            lowGravity.enabled = true;
        }
        if (gameManager.TallEnemies)
        {
            tallEnemies.enabled = true;
        }
        if (gameManager.NegativeCoins)
        {
            negativeCoins.enabled = true;
        }
        StartCoroutine(disableRulesText());

        KeyPickup.onRuleChange += ruleChangeText;
    }

    public void ruleChangeText()
    {
        keyAcquired.enabled = true;
        
        if (gameManager.LowGravity)
        {
            lowGravity.enabled = true;
        }
        if (gameManager.TallEnemies)
        {
            tallEnemies.enabled = true;
        }
        if (gameManager.NegativeCoins)
        {
            negativeCoins.enabled = true;
        }
    }

    IEnumerator disableRulesText()
    {
        yield return new WaitForSeconds(delay);
        lowGravity.enabled = false;
        tallEnemies.enabled = false;
        negativeCoins.enabled = false;
        yield return null;
    }
}
