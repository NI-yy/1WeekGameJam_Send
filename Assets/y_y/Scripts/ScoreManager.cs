using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float current_score = 0f;
    private float score = 0f;

    [SerializeField] GameObject timeManager;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = current_score.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalScore()
    {
        score = current_score + timeManager.GetComponent<TimeManager>().GetTime();
        Debug.Log("total_score: " + score);
    }

    public void AddScore(float point)
    {
        current_score += point;
        scoreText.text = current_score.ToString("F1");
    }
}
