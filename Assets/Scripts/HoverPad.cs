using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPad : MonoBehaviour
{

    private GameObject Player;

    //public Rigidbody playerRB;

    private ThirdPersonController playerController;

    private float objDistance = 0f;
    public float boxHeight;

    public float hoverForce = 20;

    private AudioSource hoverAudio;
    public AudioClip hoverSound;

    public float dampForce = 20;

    public Vector3 platformDirection = new Vector3(0,1,0);

    public GameObject aimPoint;

    public float hoverMagnitude = 0;

    public float latFactor = 0.5f;
    public BoxCollider hoverArea;
    public BoxCollider floor;
    

    // Start is called before the first frame update
    void Start()
    {
        hoverAudio = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<ThirdPersonController>();
        
        boxHeight = hoverArea.size.y;

        platformDirection = aimPoint.transform.position - transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            if((Player.transform.position.y - transform.position.y < 0.5) && (Player.transform.position.y - transform.position.y > 0))
            {
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 0.5f, Player.transform.position.z);
            }
            
            objDistance = Vector3.Distance(Player.transform.position, transform.position);

            platformDirection = aimPoint.transform.position - transform.position;

            hoverMagnitude = hoverForce * (boxHeight - objDistance) / boxHeight;

            playerController._verticalVelocity = playerController._verticalVelocity + hoverMagnitude * platformDirection.y * Time.deltaTime;

            //playerController.extraMoveDirection = new Vector3(platformDirection.x, 0, platformDirection.z) * hoverMagnitude * latFactor;


        //Dampening:

            playerController._verticalVelocity += -playerController._verticalVelocity * dampForce * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            //playerController.extraMoveDirection = new Vector3(0, 0, 0);
        }
    }




    
}
