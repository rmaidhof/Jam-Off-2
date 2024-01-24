using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPickup : MonoBehaviour
{

    public GameObject pickupEffect;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupBonus();
            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    public abstract void PickupBonus();
}
