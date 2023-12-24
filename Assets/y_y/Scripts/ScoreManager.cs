using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float current_score = 0;
    private float score = 0;

    [SerializeField] GameObject timeManager;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        current_score = 0f;
        score = 0f;
        scoreText.text = current_score.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalScore()
    {
        score = current_score + (timeManager.GetComponent<TimeManager>().GetTime() / 10f);
        return score;
    }

    public void AddScore(float point)
    {
        current_score += point;
        scoreText.text = current_score.ToString("F0");
    }
}
