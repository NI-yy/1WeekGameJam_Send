using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClearUIManager_yy : MonoBehaviour
{
    [SerializeField] GameObject ClearCanvas;
    [SerializeField] TextMeshProUGUI Clear_text_score;
    [SerializeField] GameObject ClearUI_NextButton;
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject SceneTransitionManager;
    public bool is_final = false;
    int currentGameStageNum;

    int currentSceneIndex;
    private SceneTransition _sceneTransition;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentGameStageNum = GameManager.GetComponent<StageNumManager>().GetCurrentStageNum();
        //Clear_text_score.text = "Stage" + currentGameStageNum.ToString() + "\nScore: ";
        _sceneTransition = SceneTransitionManager.GetComponent<SceneTransition>();
    }

    public void ActivateClearUI(float score)
    {
        Time.timeScale = 0;
        ClearCanvas.SetActive(true);
        Clear_text_score.text += score.ToString("F0");

        if (is_final)
        {
            ClearUI_NextButton.SetActive(false);
        }
    }

    public void ClickNextButton()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(currentSceneIndex + 1);
        _sceneTransition.SceneTransitionStart(currentGameStageNum + 1);
    }

    public void ClickRetryButton()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(currentSceneIndex);
        _sceneTransition.SceneTransitionStart(currentGameStageNum);
    }

    public void ClickStageSelectButton()
    {
        Time.timeScale = 1;
        _sceneTransition.SceneTransStartNoStage("LevelSelect");
    }

    public void ClickTitleButton()
    {
        Time.timeScale = 1;
        _sceneTransition.SceneTransStartNoStage("TitleScene_yy");
    }

    public void ResetScoreText()
    {
        Clear_text_score.text = "Stage01\nScore: ";
    }
}
