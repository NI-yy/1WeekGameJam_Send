using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalloonGimicController : MonoBehaviour
{

    [SerializeField] GameObject presentBoxes;
    [SerializeField] GameObject gimic_image;

    [SerializeField] Vector2 targetPos;
    private Vector2 initialPosition; // 初期位置
    public float moveTime = 1.0f;    // 移動にかける時間
    private float elapsedTime = 0f; // 経過時間

    private Animator anim;

    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        anim = gimic_image.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
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
                presentBoxes.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(WaitForOneSecond());
            }
        }
        else
        {

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PresentBox")
        {
            //presentBoxes.SetActive(true);
            anim.enabled = true;
            flag = true;
        }
    }

    IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gimic_image);
    }
}
