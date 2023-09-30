using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Storm : MonoBehaviour
{
    
    public HealthManager HealthManager;
    public GameObject eyeOfTheStorm;
    public float stormSpeed = 5f;
    public float pauseTime = 5f;
    public float minTravelDist = 10f;
    public float maxTravelDist = 20f;
    

    private GameObject Player;
    private Vector3 oldPosition;
    private Vector3 newPosition = new Vector3(10, 0, 10);
    private float remainingDistance;
    private float elapsedTime = 0;
    private float elapsedTimePercent = 0;
    private float moveTime;
    private float moveDistance;
    private bool stormPaused = true;
    private float travelDist;
    private float travelAngle;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        TargetNextPlace();
        StartCoroutine(PauseMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if(stormPaused == false)
        {
            MoveStorm();
        }
        
    }

    private void TargetNextPlace()
    {
        elapsedTime = 0;      
        oldPosition = transform.position;
        travelDist = Random.Range(minTravelDist, maxTravelDist);
        travelAngle = Random.Range(0f, 2*Mathf.PI);
        
        newPosition = new Vector3(transform.position.x + travelDist * Mathf.Cos(travelAngle), 0, transform.position.z + travelDist * Mathf.Sin(travelAngle));
        moveDistance = Vector3.Distance(oldPosition, newPosition);
        moveTime = moveDistance / stormSpeed;
    }
    
    private void MoveStorm()
    {
        elapsedTime += Time.deltaTime;
        elapsedTimePercent = elapsedTime / moveTime;
        
        transform.position = Vector3.Lerp(oldPosition, newPosition, elapsedTimePercent);

        if(elapsedTimePercent >= 1)
        {
            TargetNextPlace();
            StartCoroutine(PauseMovement());
        }
    }

    IEnumerator PauseMovement()
    {
        stormPaused = true;
        yield return new WaitForSeconds(pauseTime);
        stormPaused = false;
    }

}
