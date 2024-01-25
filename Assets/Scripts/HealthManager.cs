using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject victoryMenuCanvas;


    public int maxHealth;
    public int startHealth = 3;

    public int currentHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI finalScoreText;
    public ThirdPersonController thePlayer;
    

    public float invincibilityLength;
    public float invincibilityCounter;

    public Renderer[] playerRenderers;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    private AudioSource playerAudio;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public GameObject healFX;
    private GameManager gameManager;

    public static HealthManager instance;


    private void Awake()
    {
                
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        thePlayer = FindObjectOfType<ThirdPersonController>();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (thePlayer == null)
        {
            thePlayer = FindObjectOfType<ThirdPersonController>();
        }

        gameOverCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        victoryMenuCanvas.SetActive(false);

        UpdateHealthText();

        respawnPoint = thePlayer.transform.position;
        playerRenderers = thePlayer.GetComponentsInChildren<Renderer>();
        invincibilityCounter = 0;
        if(currentHealth <=0)
        {
            currentHealth = startHealth;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;

        playerAudio = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                foreach(Renderer playerRenderer in playerRenderers)
                {
                    playerRenderer.enabled = !playerRenderer.enabled;

                }
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                foreach(Renderer playerRenderer in playerRenderers)
                {
                    playerRenderer.enabled = true;

                }
            }
        }

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }


    public void HurtPlayer(int damage, Transform damageSource)
    {
        Vector3 hitDirection;
            
        hitDirection = thePlayer.transform.position - damageSource.position;
        Vector2 hitDirection2d = new Vector2(hitDirection.x, hitDirection.z);

        hitDirection2d = hitDirection2d.normalized;
        hitDirection = new Vector3(hitDirection2d.x, 0, hitDirection2d.y);
        
        HurtPlayer(damage, hitDirection);
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            if (currentHealth == 0)
            {
                Respawn();
            }
            else
            {
                playerAudio.PlayOneShot(hurtSound, 0.5f);

                thePlayer.KnockBack(direction);
                invincibilityCounter = invincibilityLength;
                //playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
            UpdateHealthText();
        }
    }

    public void HurtPlayer(int damage)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            if (currentHealth == 0)
            {
                Respawn();
            }
            else
            {
                playerAudio.PlayOneShot(hurtSound, 0.5f);
                invincibilityCounter = invincibilityLength;
                //playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
            UpdateHealthText();
        }
    }

    public void Respawn()
    {
        
        
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
        
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        playerAudio.PlayOneShot(deathSound, 0.5f);

        yield return new WaitForSeconds(respawnLength);

        //isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        //isFadeToBlack = false;
        //isFadeFromBlack = true;

        isRespawning = false;

        HandleDeath();
        //ReloadGame();

        /*
        thePlayer.gameObject.SetActive(true);
        
        GameObject player = GameObject.Find("Player");

        CharacterController charController = player.GetComponent<CharacterController>();

        charController.enabled = false;

        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;

        //thePlayer.forwardSpeed = 0;
        //thePlayer.rightSpeed = 0;

        charController.enabled = true;
        
        invincibilityCounter = invincibilityLength;

        playerRenderer.enabled = false;

        flashCounter = flashLength;
        */
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Instantiate(healFX, thePlayer.transform);
        UpdateHealthText();
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void ReloadGame()
    {
        Debug.Log("reload game called");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        gameManager.currentScore = 0;
        gameManager.redKeyAcquired = false;
        gameManager.blueKeyAcquired = false;
        gameManager.greenKeyAcquired = false;
        gameManager.LowGravity = false;
        gameManager.TallEnemies = false;
        gameManager.NegativeCoins = false;

        SceneManager.LoadScene(0);
        currentHealth = startHealth;
        
    }

    public void HandleDeath()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finalScoreText.text = "Final Score: " + gameManager.currentScore;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        

    }

    public void UpdateHealthText()
    {
        healthText.text = "Lives: " + currentHealth;

    }

    public void GameVictory()
    {
        Time.timeScale = 0;
        victoryMenuCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
