using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBoxCollider : MonoBehaviour
{
    private BoxCollider boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnOnBoxCollider());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TurnOnBoxCollider()
    {
        boxCollider = GetComponent<BoxCollider>();
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
        yield return null;
    }
}
