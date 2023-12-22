using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private int score_box = 0;
    [SerializeField] GameObject TimeManager;
    [SerializeField] GameObject ScoreManager;
    [SerializeField] GameObject ClearUIManager;

    private ScoreManager _scoreManager;

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
        if(obj.tag == "Player")
        {
            Debug.Log("Goal");
            float total_score = _scoreManager.CalScore();
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
