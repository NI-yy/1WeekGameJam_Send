using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParachuteGimicController : MonoBehaviour
{
    [SerializeField] Vector2 targetPos;
    public bool movedFlag = false;
    [SerializeField] GameObject[] presents_in;
    [SerializeField] GameObject Wall_right;

    private int array_num;
    private int index = 0;
    private Vector2 initialPosition; // 初期位置
    public float moveTime = 1.0f;    // 移動にかける時間
    private float elapsedTime = 0f; // 経過時間

    private bool flag = true;

    private string tag_1 = "PresentBox";
    private string tag_2 = "present_dummy";

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        index = 0;
        array_num = presents_in.Length;
        Debug.Log(presents_in[0]);
        StartCoroutine(WaitForOneSecond());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movedFlag)
        {
            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            // 移動時間に対する割合を計算（0～1の範囲）
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            // 指定された時間内にオブジェクトを移動させる
            transform.position = Vector2.Lerp(initialPosition, targetPos, t);

            // 移動が完了したら終了
            if (t >= 1.0f && flag)
            {
                ActivateIneerPresent();
                GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }

    void ActivateIneerPresent()
    {
        Wall_right.SetActive(false);
        for (int i = 0; i < index; i++)
        {
            Debug.Log((presents_in[i], i));
            presents_in[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            presents_in[i].tag = tag_1;
        }

        flag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PresentBox")
        {
            Destroy(collision.gameObject);
            presents_in[index].SetActive(true);
            index++;
        }
    }


    IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(5f);
        movedFlag = true;
    }
}
