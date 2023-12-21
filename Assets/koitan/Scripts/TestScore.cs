using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    [SerializeField]
    int score = 100;
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.SaveAndSendScoreOnStage(0, score);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
