using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{

    public GameObject sceneTransitionImagesIn;
    public GameObject sceneTransitionImagesOut;
    public static bool initFlag = true;

    [SerializeField] GameObject TitleCanvas;
    [SerializeField] GameObject VolumeSettingCanvas;
    [SerializeField] AudioClip buttonSE;

    private void Start()
    {
        if (initFlag)
        {
            initFlag = false;
        }
        else
        {
            sceneTransitionImagesOut.SetActive(true);
        }
    }

    public void ClickStageSelectButton()
    {
        ButtonSEManager_yy seManager = ButtonSEManager_yy.Instance;
        seManager.SettingPlaySE();
        sceneTransitionImagesIn.GetComponent<SceneTransition>().SceneTransitionStart("LevelSelect");
        // SceneManager.LoadScene("LevelSelect");
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
