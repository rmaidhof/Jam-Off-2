using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    GameObject Player;
    ThirdPersonController playerController;

    public float bounceVelocity = 20f;

    private AudioSource bounceAudio;
    public AudioClip bounceSound;
    
    // Start is called before the first frame update
    void Start()
    {
        bounceAudio = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            playerController.SetVerticalVel(bounceVelocity);
            bounceAudio.PlayOneShot(bounceSound, 1.0f);
            
        }
    }
}
