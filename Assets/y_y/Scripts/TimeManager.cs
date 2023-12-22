using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Dynamic;

public class TimeManager : MonoBehaviour
{
    public float timeLimit = 60.0f; // タイマーの制限時間（秒）
    private float timer; // 現在のタイマーの経過時間

    public TextMeshProUGUI timerText; // UIテキスト要素（UnityのCanvas内に配置されている必要があります）

    [SerializeField] GameObject GameOverUIManager;

    private bool timeIsUp = false;

    void Start()
    {
        timer = timeLimit;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!(timeIsUp))
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        

        if (timer <= 0 && !(timeIsUp))
        {
            // タイマーが終了した時の処理
            timer = 0;
            GameOverUIManager.GetComponent<GameOverManager_yy>().ActivateGameOverUI();
            timeIsUp = true;
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = timer.ToString("F0"); // タイマーをテキストに表示（小数点以下2桁まで表示）
        }
    }

    public float GetTime()
    {
        return timer;
    }
}


