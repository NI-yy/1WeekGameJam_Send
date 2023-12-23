using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager_yy : MonoBehaviour
{
    [SerializeField] GameObject GameOverCanvas;
    [SerializeField] GameObject PoseUIManager;
    [SerializeField] GameObject ClearUIManager;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ActivateGameOverUI()
    {
        PoseUIManager.GetComponent<PoseUIManager>().is_over = true;
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);
    }

    public void ClickRetryButton()
    {
        //ClearUIManager.GetComponent<ClearUIManager_yy>().ResetScoreText();
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ClickStageSelectButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StageSelectScene_yy");
    }

    public void ClickTitleButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene_yy");
    }
}
