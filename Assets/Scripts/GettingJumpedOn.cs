using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingJumpedOn : MonoBehaviour
{

    private GameObject Player;
    private ThirdPersonController playerController;
    public GameObject parent;

    public float bounceVelocity = 20f;

    private AudioSource bounceAudio;
    public AudioClip bounceSound;

    private BoxCollider boxCollider;

    public int damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        bounceAudio = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<ThirdPersonController>();
        
        StartCoroutine(EnableBoxCollider());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Got jumped on");
            playerController.SetVerticalVel(bounceVelocity);
            bounceAudio.PlayOneShot(bounceSound, 1.0f);

            //Deals damage:
            parent.GetComponent<EmeraldAI.IDamageable>().Damage(damageAmount, Player.transform, 0);
        }
    }

    //Enables box collider because emerald AI disables it when level is run
    IEnumerator EnableBoxCollider()
    {
        boxCollider = GetComponent<BoxCollider>();
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
        yield return null;
    }
}
