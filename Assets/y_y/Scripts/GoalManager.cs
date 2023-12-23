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
    [SerializeField] GameObject PoseUIManager;

    private ScoreManager _scoreManager;
    float total_score = 0f;
    private bool is_over = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ResetScore");
        total_score = 0f;
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
            if (!(is_over))
            {
                Destroy(obj);
                PoseUIManager.GetComponent<PoseUIManager>().is_over = true;
                total_score = _scoreManager.CalScore();
                Debug.Log(("Goal", total_score, this.gameObject));
                SaveManager.SaveAndSendScoreOnStage(StageNumManager.current_stage_num, (int)total_score);
                ClearUIManager.GetComponent<ClearUIManager_yy>().ActivateClearUI(total_score);

                is_over = true;
            }
            
        }
        else if (obj.tag == "PresentBox")
        {
            Destroy(obj);
            _scoreManager.AddScore(10f);
        }
        else if (obj.tag == "BigPresentBox")
        {
            Destroy(obj);
            _scoreManager.AddScore(50f);
        }
    }

    public void ResetScore()
    {

    }
}
