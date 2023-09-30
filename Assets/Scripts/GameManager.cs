using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public int currentScore;
    public TextMeshProUGUI scoreText;
    private AudioSource gameAudio;
    public AudioClip gainScoreSound;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = "Score: " + currentScore;
        gameAudio.PlayOneShot(gainScoreSound, 0.5f);
    }
}
