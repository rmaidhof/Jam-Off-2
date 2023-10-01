using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireTime = 5f;
    private float fireCount = 0;
    private AudioSource fireAudio;
    public AudioClip fireSound;
    
    // Start is called before the first frame update
    void Start()
    {
        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        fireCount += Time.deltaTime;
        if( fireCount >= fireTime)
        {
            fireCount = 0;
            Instantiate(projectile, spawnPoint);
            if (fireSound)
            {
                fireAudio.PlayOneShot(fireSound, 0.5f);
            }
        }
    }
}
