using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public bool timerEnded = false;
    public Text timeText;
    public GameObject player;
    public AudioSource music;
    public int startingPitch = 1;
    public int timeToIncrease = 10;
    
    private PlayerController _playerScript;

    private void Start()
    {
        _playerScript = player.GetComponent<PlayerController>();
        // Starts the timer automatically after Countdown
        music.pitch = startingPitch;

        
    }

    void Update()
    {
        if (!_playerScript.countdownRunning)
        {
        timerIsRunning = true;
        }
        
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                if (_playerScript.count < 12){
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                if (timeRemaining<= 8){
                    timeText.color = Color.red;
                    if (timeText.fontStyle == FontStyle.Normal){
                        timeText.fontStyle = FontStyle.Bold;
                    } else {
                        timeText.fontStyle = FontStyle.Normal;
                    }
                    if (music.pitch > 0 & music.pitch <= 1.8)
                    {
                        music.pitch += Time.deltaTime * startingPitch / timeToIncrease;
                    }
                }
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerEnded = true;
                timerIsRunning = false;

            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}