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
    private Vector2 initialPosition; // �����ʒu
    public float moveTime = 1.0f;    // �ړ��ɂ����鎞��
    private float elapsedTime = 0f; // �o�ߎ���

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
            // �o�ߎ��Ԃ��X�V
            elapsedTime += Time.deltaTime;

            // �ړ����Ԃɑ΂��銄�����v�Z�i0�`1�͈̔́j
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            // �w�肳�ꂽ���ԓ��ɃI�u�W�F�N�g���ړ�������
            transform.position = Vector2.Lerp(initialPosition, targetPos, t);

            // �ړ�������������I��
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
