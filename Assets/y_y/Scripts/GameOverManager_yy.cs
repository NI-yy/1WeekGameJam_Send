using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager_yy : MonoBehaviour
{
    [SerializeField] GameObject GameOverCanvas;

    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ActivateGameOverUI()
    {
        Debug.Log("ActivateGameOverUI");
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);
    }

    public void ClickRetryButton()
    {
        Time.timeScale = 1;
        Debug.Log(Time.timeScale);
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
