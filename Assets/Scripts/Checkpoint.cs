using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

    
    
    // Start is called before the first frame update
    void Start()
    {
        theHealthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach(Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }
        
        theRend.material = cpOn;
    }

    public void CheckpointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") )
        {
            theHealthMan.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    }
}
