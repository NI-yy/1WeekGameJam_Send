using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager_yy : MonoBehaviour
{
    private string[] temp = { "Stage1", "Stage2", "Stage3", "Stage4", "Stage5", "Stage6", "Stage7", "Stage8", "Stage9" };
    List<string> stageSceneNames = new List<string>();
    Slider volumeSlider;
    private bool slider_exist = false;

    //シングルトン設定ここから
    static public BGMManager_yy instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    //シングルトン設定ここまで




    public AudioSource TitleStageSelect_BGM;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource StageBGM;//AudioSource型の変数B_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ

    private string beforeScene;//string型の変数beforeSceneを宣言 

    void Start()
    {
        beforeScene = "TitleScene_yy";//起動時のシーン名 を代入しておく
        TitleStageSelect_BGM.Play();//A_BGMのAudioSourceコンポーネントに割り当てたAudioClipを再生

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        for (int i = 0; i < temp.Length; i++)
        {
            stageSceneNames.Add(temp[i]);
        }

        GameObject obj = GameObject.Find("ParentSettingCanvas");
        if(obj != null)
        {
            GameObject obj_slider = obj.transform.Find("SettingCanvas/VolumeSlider").gameObject;
            Debug.Log(obj_slider);
            slider_exist = true;
            volumeSlider = obj_slider.GetComponent<Slider>();
        }
        else
        {
            slider_exist = false;
        }
    }

    private void Update()
    {
        if (slider_exist)
        {
            TitleStageSelect_BGM.volume = volumeSlider.value;
            StageBGM.volume = volumeSlider.value / 2;
        }
        
    }



    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        GameObject obj = GameObject.Find("ParentSettingCanvas");
        if (obj != null)
        {
            GameObject obj_slider = obj.transform.Find("SettingCanvas/VolumeSlider").gameObject;
            Debug.Log(obj_slider);
            slider_exist = true;
            volumeSlider = obj_slider.GetComponent<Slider>();
        }
        else
        {
            slider_exist = false;
        }


        //シーンがどう変わったかで判定
        //Scene1からScene2へ
        if (beforeScene == "LevelSelect" && stageSceneNames.Contains(nextScene.name))
        {
            TitleStageSelect_BGM.Stop();
            StageBGM.Play();
        }

        // Scene1からScene2へ
        if (stageSceneNames.Contains(beforeScene) && nextScene.name == "TitleScene_yy")
        {
            TitleStageSelect_BGM.Play();
            StageBGM.Stop();
        }

        if (stageSceneNames.Contains(beforeScene) && nextScene.name == "LevelSelect")
        {
            TitleStageSelect_BGM.Play();
            StageBGM.Stop();
        }


        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }

    public void SoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        TitleStageSelect_BGM.volume = newSliderValue;
        StageBGM.volume = newSliderValue / 2;
    }
}
