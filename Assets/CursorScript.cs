using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    /// <summary>
    /// ëIëéà ç∂Ç©ÇÁ0
    /// </summary>
    [SerializeField] GameObject[] select;
    /// <summary>
    /// åªç›ÇÃëŒè€î‘çÜ
    /// </summary>
    [SerializeField] int cursor = 0;
    private void Start()
    {
        //CursorUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (cursor < select.Length - 1) {
                cursor ++;
                CursorUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (cursor == 0)
            {
                cursor = 0;
                CursorUpdate();
            }
            if (cursor > 0)
            {
                cursor --;
                CursorUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            select[cursor].GetComponent<Button>().onClick.Invoke();
        }
    }
    private void CursorUpdate()
    {
        gameObject.transform.position = select[cursor].transform.position;
    }
}
