using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelCuror_yy : MonoBehaviour
{
    /// <summary>
    /// 選択肢 左から0
    /// </summary>
    [SerializeField] GameObject[] select;
    /// <summary>
    /// 現在の対象番号
    /// </summary>
    [SerializeField] int cursor = 0;

    /// <summary>
    /// 1つ前の対象番号
    /// </summary>
    [SerializeField] int cursor_prev = 0;

    /// <summary>
    /// 最初だけ別処理
    /// </summary>
    private bool is_init = true;
    private void Start()
    {
        //CursorUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!(is_init))
            {
                cursor_prev = cursor;
            }
            
            if (cursor < select.Length - 1)
            {
                if (select[cursor + 1].activeSelf)
                {
                    cursor++;
                    CursorUpdate();
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!(is_init))
            {
                cursor_prev = cursor;
            }

            if (cursor == 0)
            {
                cursor = 0;
                CursorUpdate();
            }
            if (cursor > 0)
            {
                cursor--;
                CursorUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            select[cursor].GetComponent<SelectLevel>().StartLevel();
        }
    }

    private void CursorUpdate()
    {
        Debug.Log((cursor_prev, cursor));
        if (select[cursor].activeSelf)
        {
            select[cursor].GetComponent<SelectLevel>().CharacterMove();
            select[cursor].GetComponent<SelectLevel>().StartAnimation();

            if (!(is_init))
            {
                if (cursor != cursor_prev)
                {
                    select[cursor_prev].GetComponent<SelectLevel>().StopAnimation();
                }
            }
        }
        
    }
}
