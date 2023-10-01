using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public int currentScore;
    public TextMeshProUGUI scoreText;
    private AudioSource gameAudio;
    
    public int scoreTimeScore = 1;
    public float scoreTime = 1f;
    private float scoreTimeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeScore();
    }

    private void TimeScore()
    {
        scoreTimeCount += Time.deltaTime;
        if(scoreTimeCount >= scoreTime)
        {
            AddScore(scoreTimeScore);
            scoreTimeCount = 0;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = "Score: " + currentScore;
        
    }
}
