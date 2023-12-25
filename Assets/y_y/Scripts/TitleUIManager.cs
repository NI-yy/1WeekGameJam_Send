using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{

    public GameObject sceneTransitionImagesIn;
    public GameObject sceneTransitionImagesOut;
    public static bool initFlag = false;

    private void Start()
    {
        if (initFlag)
        {
            sceneTransitionImagesOut.SetActive(true);
        }
        else
        {
            initFlag = false;
        }
    }

    public void ClickStageSelectButton()
    {
        sceneTransitionImagesIn.GetComponent<SceneTransition>().SceneTransitionStart("LevelSelect");
        // SceneManager.LoadScene("LevelSelect");
    }
}
