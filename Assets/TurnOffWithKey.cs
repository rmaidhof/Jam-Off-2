using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffWithKey : MonoBehaviour
{
    public GameManager gameManager;
    public bool red;
    public bool green;
    public bool blue;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disable());
    }

    IEnumerator disable()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = FindObjectOfType<GameManager>();
        if (red && gameManager.redKeyAcquired == true)
        {
            gameObject.SetActive(false);
        }
        if (green && gameManager.greenKeyAcquired == true)
        {
            gameObject.SetActive(false);
        }
        if (blue && gameManager.blueKeyAcquired == true)
        {
            gameObject.SetActive(false);
        }
        yield return null;
    }

    
}
