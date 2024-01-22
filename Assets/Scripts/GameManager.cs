using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ThirdPersonController playerController;

    public int currentScore;
    public TextMeshProUGUI scoreText;
    private AudioSource gameAudio;
    
    public bool coinsGiveHealth = true;
    public int maxCoins = 20;
    HealthManager healthManager;

    [Header("Rules")]

    //Low gravity rule:
    [SerializeField]
    private bool lowGravity = false;
    public bool LowGravity
    {
        get
        {
            return lowGravity;
        }
        set
        {
            lowGravity = value;
            UpdateGravity();
        }
    }
    public float gravityMultiplier = 0.5f;
    private float normalGravity;
    private float normalJumpHeight;
    private float normalPlayerGravity;

    //Tall enemies rule:
    [SerializeField]
    private bool tallEnemies = false;
    public bool TallEnemies
    {
        get
        {
            return tallEnemies;
        }
        set
        {
            tallEnemies = value;
            UpdateEnemies();
        }
    }

    

    public float enemyHeightMultiplier = 2f;

    //Negative Coins:
    [SerializeField]
    private bool negativeCoins = false;
    public bool NegativeCoins
    {
        get
        {
            return negativeCoins;
        }
        set
        {
            negativeCoins = value;
            UpdateCoins();
        }
    }
    
    public delegate void NegativeCoinsEvent();
    public static event NegativeCoinsEvent onNegativeCoins;
    public delegate void PositiveCoinsEvent();
    public static event PositiveCoinsEvent onPositiveCoins;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        normalGravity = Physics.gravity.y;
        
        
        playerController = FindObjectOfType<ThirdPersonController>();
        normalJumpHeight = playerController.JumpHeight;
        normalPlayerGravity = playerController.Gravity;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(playerController == null)
        {
            
            playerController = FindObjectOfType<ThirdPersonController>();

        }

        UpdateScoreText();
        UpdateGravity();

    }

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        healthManager = GetComponent<HealthManager>();
        

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NegativeCoins = !NegativeCoins;
        }
        

    }

    private void UpdateCoins()
    {
        if (NegativeCoins)
        {
            onNegativeCoins();
        }
        else
        {
            onPositiveCoins();
        }
    }


    private void UpdateEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(TallEnemies)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x, enemyHeightMultiplier, enemy.transform.localScale.z);
            }
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x, 1, enemy.transform.localScale.z);
            }
        }
    }

    private void UpdateGravity()
    {
        if (lowGravity)
        {
            Physics.gravity = new Vector3(0, normalGravity * gravityMultiplier, 0);
            playerController.Gravity = normalPlayerGravity * gravityMultiplier;
            playerController.JumpHeight = normalJumpHeight / gravityMultiplier;
        }
        else
        {
            Physics.gravity = new Vector3(0, normalGravity, 0);
            playerController.Gravity = normalPlayerGravity;
            playerController.JumpHeight = normalJumpHeight;
        }
    }

    

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        if(coinsGiveHealth && currentScore >= maxCoins)
        {
            healthManager.HealPlayer(1);
            currentScore -= maxCoins;
        }
        else if(coinsGiveHealth && currentScore< 0)
        {
            healthManager.HurtPlayer(1);
            currentScore += maxCoins;
        }
        UpdateScoreText();

    }

    private void UpdateScoreText()
    {
        if(coinsGiveHealth)
        {
            scoreText.text = "Coins: " + currentScore + "/" + maxCoins;
        }
        else
        {
            scoreText.text = "Coins: " + currentScore;

        }
    }

}
