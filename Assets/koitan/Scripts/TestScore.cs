using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScore : MonoBehaviour
{
    [SerializeField]
    int score = 100;
    [SerializeField]
    TextMeshProUGUI[] texts;
    [SerializeField]
    Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = $"{i + 1}\nScore : {SaveManager.GetScoreOnStage(i + 1)}";
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < SaveManager.GetStageFinishNumber() + 1)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
