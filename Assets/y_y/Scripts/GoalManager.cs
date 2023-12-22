using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GoalManager : MonoBehaviour
{
    private int score_box = 0;
    [SerializeField] GameObject TimeManager;
    [SerializeField] GameObject ScoreManager;
    [SerializeField] GameObject ClearUIManager;
    [SerializeField] StageNumManager StageNumManager;

    private ScoreManager _scoreManager;
    float total_score = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _scoreManager = ScoreManager.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            Debug.Log("Goal");
            total_score = _scoreManager.CalScore();
            SaveManager.SaveAndSendScoreOnStage(StageNumManager.current_stage_num, (int)total_score);
            ClearUIManager.GetComponent<ClearUIManager_yy>().ActivateClearUI(total_score);
        }
        else if (obj.tag == "PresentBox")
        {
            Destroy(obj);
            _scoreManager.AddScore(10f);
            Debug.Log("Score: " + score_box);
        }
        else if (obj.tag == "BigPresentBox")
        {
            Destroy(obj);
            _scoreManager.AddScore(50f);
            Debug.Log("Score: " + score_box);
        }
    }
}
