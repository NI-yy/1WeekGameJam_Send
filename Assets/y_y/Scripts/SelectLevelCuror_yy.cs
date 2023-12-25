using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelCuror_yy : MonoBehaviour
{
    /// <summary>
    /// ëIëéà ç∂Ç©ÇÁ0
    /// </summary>
    [SerializeField] GameObject[] select;
    /// <summary>
    /// åªç›ÇÃëŒè€î‘çÜ
    /// </summary>
    [SerializeField] int cursor = 0;

    /// <summary>
    /// 1Ç¬ëOÇÃëŒè€î‘çÜ
    /// </summary>
    [SerializeField] int cursor_prev = 0;

    /// <summary>
    /// ç≈èâÇæÇØï èàóù
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
