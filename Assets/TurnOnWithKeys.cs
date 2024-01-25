using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnWithKeys : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enable());
    }

    IEnumerator enable()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.redKeyAcquired && gameManager.blueKeyAcquired && gameManager.greenKeyAcquired)
        {
            gameObject.SetActive(true);
        }
        yield return null;
    }
}
