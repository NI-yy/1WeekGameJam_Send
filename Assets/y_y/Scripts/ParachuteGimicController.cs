using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParachuteGimicController : MonoBehaviour
{
    [SerializeField] Vector2 targetPos;
    public bool movedFlag = false;
    [SerializeField] GameObject[] presents_in;

    private int array_num;
    private int index = 0;
    private Vector2 initialPosition; // �����ʒu
    public float moveTime = 1.0f;    // �ړ��ɂ����鎞��
    private float elapsedTime = 0f; // �o�ߎ���

    private bool flag = true;

    private string tag_1 = "PresentBox";
    private string tag_2 = "present_dummy";

    [SerializeField] GameObject Hukuro;

    [SerializeField] float kikyu_speed;
    private bool kikyu_flag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        index = 0;
        array_num = presents_in.Length;
        StartCoroutine(WaitForOneSecondForFlag(5f));
        
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
                movedFlag = false;
                Hukuro.GetComponent<Animator>().enabled = true;
                ActivateIneerPresent();
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(WaitForOneSecondAfterMoved(1f));
                
                
            }

        }
    }

    void ActivateIneerPresent()
    {
        for (int i = 0; i < index; i++)
        {
            //presents_in[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            presents_in[i].GetComponent<Rigidbody2D>().simulated = true;
            presents_in[i].GetComponent<BoxCollider2D>().enabled = true;
            Color currentColor = presents_in[i].GetComponent<SpriteRenderer>().color;
            currentColor.a = 1.0f; // 0����1�͈̔͂Ŏw�肷�邽�߂ɕϊ��i255�Ŋ���j
            presents_in[i].GetComponent<SpriteRenderer>().color = currentColor; // �ݒ肵���F����K�p
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


    IEnumerator WaitForOneSecondForFlag(float time)
    {
        yield return new WaitForSeconds(time);
        movedFlag = true;
    }

    IEnumerator WaitForOneSecondAfterMoved(float time)
    {
        yield return new WaitForSeconds(time);
        Hukuro.SetActive(false);
        kikyu_flag = true;
    }

    private void FixedUpdate()
    {
        if (kikyu_flag)
        {
            Vector2 movement = Vector2.up * kikyu_speed * Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movement);
        }
        
    }
}
