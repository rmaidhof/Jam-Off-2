using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOfTheStorm : MonoBehaviour
{

    private GameObject Player;
    public HealthManager HealthManager;
    public bool playerInStorm = false;
    public float hurtTargetTime = 2f;
    public float hurtTime;
    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        hurtTime = hurtTargetTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInStorm)
        {
            PlayerInStorm();
        }
    }

    private void PlayerInStorm()
    {
        hurtTime -= Time.deltaTime;
            if (hurtTime <= 0)
            {
                HealthManager.HurtPlayer(damage);
                hurtTime = hurtTargetTime;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            playerInStorm=true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            playerInStorm = false;
            hurtTime = hurtTargetTime;

        }
    }
}
