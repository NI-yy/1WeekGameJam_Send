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
    private Vector2 initialPosition; // �����ʒu
    public float moveTime = 1.0f;    // �ړ��ɂ����鎞��
    private float elapsedTime = 0f; // �o�ߎ���

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
            // �o�ߎ��Ԃ��X�V
            elapsedTime += Time.deltaTime;

            // �ړ����Ԃɑ΂��銄�����v�Z�i0�`1�͈̔́j
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            // �w�肳�ꂽ���ԓ��ɃI�u�W�F�N�g���ړ�������
            transform.position = Vector2.Lerp(initialPosition, targetPos, t);


            // �ړ�������������I��
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
