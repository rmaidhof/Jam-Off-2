using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int damageToGive = 1;
    public Transform centerPoint;
    private Transform center;
    public bool bounceUp = false;
    private GameObject Player;
    public bool destroyOnCol = false;

    // Start is called before the first frame update
    void Start()
    {
        center = centerPoint ? centerPoint : transform;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            Vector3 hitDirection;
            if (bounceUp)
            {
                hitDirection = Vector3.up;
                //Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.5f, Player.transform.position.z);
            }
            else
            {
                hitDirection = other.transform.position - center.position;
                Vector2 hitDirection2d = new Vector2(hitDirection.x, hitDirection.z);

                hitDirection2d = hitDirection2d.normalized;
                hitDirection = new Vector3(hitDirection2d.x, 0, hitDirection2d.y);
            }
                      
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);

            if(destroyOnCol)
            {
                Destroy(this);
            }
        }
    }
}
