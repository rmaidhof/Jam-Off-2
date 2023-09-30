using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int damageToGive = 1;
    public Transform centerPoint;
    private Transform center;
    
    // Start is called before the first frame update
    void Start()
    {
        center = centerPoint ? centerPoint : transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - center.position;
            Vector2 hitDirection2d = new Vector2(hitDirection.x, hitDirection.z);
            hitDirection2d = hitDirection2d.normalized;           
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection2d);
        }
    }
}
