using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteBoxController : MonoBehaviour
{
    [SerializeField] float timeForFreeze = 1f; //パラシュートが開くまでの時間
    public bool parachuteThrown = false;

    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parachuteThrown)
        {
            time += Time.deltaTime;
            if(time >= timeForFreeze)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<BoxCollider2D>().isTrigger = false;

                time = 0f;
                parachuteThrown = false;
            }
        }
    }
}
