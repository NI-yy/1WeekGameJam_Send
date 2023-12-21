using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityroom.Api;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScoreManager
{
    static int stageCount = 9;

    /// <summary>
    /// ステージのスコアを取得します
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    /// <returns></returns>
    public static int GetScoreOnStage(int stageNum)
    {
        return PlayerPrefs.GetInt($"HighScore_Stage_{stageNum}", 0);
    }

    /// <summary>
    /// ステージのスコアをセーブします
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    /// <param name="score">スコア</param>
    /// <param name="isOnlyHighScoreWrite">ハイスコア時のみ書き込むか</param>
    public static void SaveAndSendScoreOnStage(int stageNum, int score, bool isOnlyHighScoreWrite = true)
    {
        if (isOnlyHighScoreWrite)
        {
            if (score < PlayerPrefs.GetInt($"HighScore_Stage_{stageNum}", 0))
            {
                return;
            }
        }
        PlayerPrefs.SetInt($"HighScore_Stage_{stageNum}", score);
        SendHighScore();
    }

    /// <summary>
    /// クリアしたステージ番号を取得します
    /// </summary>
    /// <returns></returns>
    public static int GetStageFinishNumber()
    {
        return PlayerPrefs.GetInt("FinishStageNum", -1);
    }

    /// <summary>
    /// クリアしたステージ番号をセーブします
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    public static void SaveStageFinishNumber(int stageNum)
    {
        PlayerPrefs.SetInt("FinishStageNum", stageNum);
    }

    static int GetScoreSum()
    {
        int sum = 0;
        for (int i = 0; i < stageCount; i++)
        {
            sum += PlayerPrefs.GetInt($"HighScore_Stage_{i}", 0);
        }
        return sum;
    }

    static void SendHighScore()
    {
        UnityroomApiClient.Instance.SendScore(0, GetScoreSum(), ScoreboardWriteMode.HighScoreDesc);
    }

#if UNITY_EDITOR
    [MenuItem("koitan/DeleteSaveData")]
    static void DeleteSaveData()
    {
        for (int i = 0; i < stageCount; i++)
        {
            PlayerPrefs.DeleteKey($"HighScore_Stage_{i}");
        }
        Debug.Log("セーブデータを消しました");
    }
#endif
}
