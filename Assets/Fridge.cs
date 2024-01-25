using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fridge : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public AudioClip chompSound;
    public AudioSource fridgeSound;
    public PlayerInput playerInput;
    public HealthManager healthManager;


    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        healthManager = FindObjectOfType<HealthManager>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(eatTheSandwich());
        }
    }

    IEnumerator eatTheSandwich()
    {
        animator.SetTrigger("open door");

        playerInput.enabled = false;


        yield return null;
    }

    public void eatSandwich()
    {
        fridgeSound.PlayOneShot(chompSound);
    }

    public void enableVictoryText()
    {
        healthManager.GameVictory();
    }

}
