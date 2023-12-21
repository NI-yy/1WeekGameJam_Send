using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Dynamic;

public class TimeManager : MonoBehaviour
{
    public float timeLimit = 60.0f; // �^�C�}�[�̐������ԁi�b�j
    private float timer; // ���݂̃^�C�}�[�̌o�ߎ���

    public TextMeshProUGUI timerText; // UI�e�L�X�g�v�f�iUnity��Canvas���ɔz�u����Ă���K�v������܂��j

    void Start()
    {
        timer = timeLimit;
        UpdateTimerDisplay();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timer <= 0)
        {
            // �^�C�}�[���I���������̏���
            timer = 0;
            Debug.Log("GameOver");
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = timer.ToString("F2"); // �^�C�}�[���e�L�X�g�ɕ\���i�����_�ȉ�2���܂ŕ\���j
        }
    }

    public float GetTime()
    {
        return timer;
    }
}


