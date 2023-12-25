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
    [SerializeField] GameObject SceneTransitionImagesIn;
    public bool is_final = false;
    int currentGameStageNum;

    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentGameStageNum = GameManager.GetComponent<StageNumManager>().GetCurrentStageNum();
        //Clear_text_score.text = "Stage" + currentGameStageNum.ToString() + "\nScore: ";
    }

    public void ActivateClearUI(float score)
    {
        Time.timeScale = 0;
        ClearCanvas.SetActive(true);
        Clear_text_score.text += score.ToString("F2");

        if (is_final)
        {
            ClearUI_NextButton.SetActive(false);
        }
    }

    public void ClickNextButton()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene(currentSceneIndex + 1);
        SceneTransitionStart("Stage" + (currentGameStageNum + 1).ToString());
    }

    public void ClickRetryButton()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene(currentSceneIndex);
        SceneTransitionStart("Stage" + currentGameStageNum.ToString());
    }

    public void ClickStageSelectButton()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene("LevelSelect");
        SceneTransitionStart("LevelSelect");
    }

    public void ClickTitleButton()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene("TitleScene_yy");
        SceneTransitionStart("TitleScene_yy");
    }

    public void ResetScoreText()
    {
        Clear_text_score.text = "Stage01\nScore: ";
    }

    private void SceneTransitionStart(string sceneName)
    {
        SceneTransitionImagesIn.SetActive(true);
        SceneTransition sceneTransition = SceneTransitionImagesIn.GetComponent<SceneTransition>();
        sceneTransition.SceneTransitionStart(sceneName);
    }
}
