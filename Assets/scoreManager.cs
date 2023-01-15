using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    //allows us to access methods from scoreManager from Any script
    public static scoreManager instance;
    public Text scoreText;
    

    int chests = 0;
    // int highscore = 0;

    private void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
       scoreText.text = chests.ToString() + " CHESTS";
      // highScoreText.text = highscore.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to update point
    public void updateScore() {
        chests += 1;
        scoreText.text = chests.ToString() + " CHESTS";
    }
}
