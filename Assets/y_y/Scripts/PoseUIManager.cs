using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseUIManager : MonoBehaviour
{
    [SerializeField] GameObject PoseCanvas;

    private bool flag = true;

    public bool is_over = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(is_over))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (flag)
                {
                    Time.timeScale = 0;
                    PoseCanvas.SetActive(true);
                    flag = false;
                }
                else
                {
                    Time.timeScale = 1;
                    PoseCanvas.SetActive(false);
                    flag = true;
                }

            }
        }
        
    }
}
