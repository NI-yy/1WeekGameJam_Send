using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager_yy : MonoBehaviour
{

    public void ClickStartButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
