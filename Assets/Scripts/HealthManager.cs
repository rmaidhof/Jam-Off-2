using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class HealthManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public ThirdPersonController thePlayer;
    

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
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


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;

        playerAudio = GetComponent<AudioSource>();

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
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
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

    public void HurtPlayer(int damage, Vector2 direction)
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
                thePlayer.KnockBack(direction);
                invincibilityCounter = invincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
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
                invincibilityCounter = invincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
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

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;

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

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
