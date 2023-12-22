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
    public bool is_final = false;

    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ClickRetryButton()
    {
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