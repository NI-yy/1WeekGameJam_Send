using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject SceneTransitionManager;
    [SerializeField] GameObject SceneTransitionImagesOut;
    [SerializeField] GameObject TitleCanvas;
    [SerializeField] GameObject VolumeSettingCanvas;
    [SerializeField] AudioClip buttonSE;

    public static bool initFlag = false;

    private void Start()
    {
        if (initFlag)
        {
            SceneTransitionImagesOut.SetActive(true);
        }
        else
        {
            initFlag = true;
        }
    }

    public void ClickButton()
    {
        ButtonSEManager_yy seManager = ButtonSEManager_yy.Instance;
        seManager.SettingPlaySE();
        SceneTransitionManager.GetComponent<SceneTransition>().SceneTransStartNoStage("LevelSelect");
    }

    public void ClickSettingButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        TitleCanvas.SetActive(false);
        VolumeSettingCanvas.SetActive(true);
    }

    public void ClickExitButton()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSE);
        TitleCanvas.SetActive(true);
        VolumeSettingCanvas.SetActive(false);
    }
}
