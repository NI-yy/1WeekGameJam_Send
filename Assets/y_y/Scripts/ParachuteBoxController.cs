using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteBoxController : MonoBehaviour
{
    [SerializeField] float timeForFreeze = 1f; //パラシュートが開くまでの時間
    public bool parachuteThrown = false;

    private float time = 0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parachuteThrown)
        {
            time += Time.deltaTime;
            if(time >= timeForFreeze)
            {
                Debug.Log("Freeze");
                // X軸とY軸の位置を固定する
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                // Z軸の回転を固定する
                rb.constraints |= RigidbodyConstraints2D.FreezeRotation;

                GetComponent<BoxCollider2D>().isTrigger = false;

                time = 0f;
                parachuteThrown = false;
            }
        }
    }
}
