using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{

    public GameObject sceneTransitionImagesIn;

    public void ClickStageSelectButton()
    {
        sceneTransitionImagesIn.GetComponent<SceneTransition>().SceneTransitionStart("LevelSelect");
        // SceneManager.LoadScene("LevelSelect");
    }
}
