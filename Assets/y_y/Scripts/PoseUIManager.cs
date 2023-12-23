using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoseUIManager : MonoBehaviour
{
    [SerializeField] GameObject PoseCanvas;
    [SerializeField] GameObject ClearUIManager;

    private bool flag = true;

    public bool is_over = false;
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(is_over))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (flag)
                {
                    Time.timeScale = 0;
                    PoseCanvas.SetActive(true);
                    flag = false;
                }
                else
                {
                    Time.timeScale = 1;
                    PoseCanvas.SetActive(false);
                    flag = true;
                }

            }
        }
        
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
