using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    public int currentGold;
    public TextMeshProUGUI goldText;
    private AudioSource gameAudio;
    public AudioClip gainGoldSound;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
        gameAudio.PlayOneShot(gainGoldSound, 0.5f);
    }
}
