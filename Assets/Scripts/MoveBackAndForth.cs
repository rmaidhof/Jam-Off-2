using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{

    public bool moveX;
    public bool moveY;
    public bool moveZ;

    public float distance;
    public float speed;
    public float pause;

    private Vector3 startPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }
}
