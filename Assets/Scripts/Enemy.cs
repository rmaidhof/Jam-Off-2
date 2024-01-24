using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int score = 1;
    public GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(findGM());

    }
    public void addScore()
    {
        gameManager.AddScore(score);
    }

    IEnumerator findGM()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = FindObjectOfType<GameManager>();
        yield return null;
    }
}
