using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public Text bestScoreText;
    public int bestScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        ShowBest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowBest()
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    public void SetBestScore(int score)
    {
        if (bestScore < score)
        {
            bestScore = score;
            ShowBest();
        }
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
}
