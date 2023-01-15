using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [Header("Component")] 
    public Text timerText;

    [Header("TimerSettings")]
    float currentTime;
    public float startMin;

    public float timerLimit;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMin * 60;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime - Time.deltaTime;

        if (currentTime <= timerLimit) {
            currentTime = timerLimit;
            updateTime();
            timerText.color = Color.red;
            enabled = false;
            // GameManager.Instance.UpdateGameState(GameState.Lose);

        }

        updateTime();
        if (currentTime == 0) {
            SceneManager.LoadScene(2);
        }
        
        
    }

    private void updateTime(){
        TimeSpan time = TimeSpan.FromSeconds(currentTime);


        timerText.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
    }
}

public enum TimerFormats {
    Whole

}


