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

    //�V���O���g���ݒ肱������
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
    //�V���O���g���ݒ肱���܂�




    public AudioSource TitleStageSelect_BGM;//AudioSource�^�̕ϐ�A_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`
    public AudioSource StageBGM;//AudioSource�^�̕ϐ�B_BGM��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`

    private string beforeScene;//string�^�̕ϐ�beforeScene��錾 

    void Start()
    {
        beforeScene = "TitleScene_yy";//�N�����̃V�[���� �������Ă���
        TitleStageSelect_BGM.Play();//A_BGM��AudioSource�R���|�[�l���g�Ɋ��蓖�Ă�AudioClip���Đ�

        //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h��o�^
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



    //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h�@
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


        //�V�[�����ǂ��ς�������Ŕ���
        //Scene1����Scene2��
        if (beforeScene == "LevelSelect" && stageSceneNames.Contains(nextScene.name))
        {
            TitleStageSelect_BGM.Stop();
            StageBGM.Play();
        }

        // Scene1����Scene2��
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


        //�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
        beforeScene = nextScene.name;
    }

    public void SoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        TitleStageSelect_BGM.volume = newSliderValue;
        StageBGM.volume = newSliderValue / 2;
    }
}
