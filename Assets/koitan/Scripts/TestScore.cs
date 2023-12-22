using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScore : MonoBehaviour
{
    [SerializeField]
    int score = 100;
    [SerializeField]
    TextMeshProUGUI[] texts;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = $"{i + 1}\nScore : {SaveManager.GetScoreOnStage(i + 1)}";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
