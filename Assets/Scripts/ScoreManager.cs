using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecound;

    public bool scoureIncreasing;


    // Use this for initialization
    void Start () {
        if(PlayerPrefs.HasKey("HightScore"))
        {
            hiScoreCount = PlayerPrefs.GetFloat("HightScore");
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (scoureIncreasing)
        {
            scoreCount += pointsPerSecound * Time.deltaTime;
        }

        if (scoreCount > hiScoreCount)
        {

            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HightScore", hiScoreCount);


        }

        scoreText.text = "Scour: " + Mathf.Round(scoreCount);
        hiScoreText.text = "Hight Scour: " + Mathf.Round(hiScoreCount);

    }

    public void AddScore (int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }

}
