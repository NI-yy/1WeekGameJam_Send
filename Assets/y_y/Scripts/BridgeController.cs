using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] Vector2 targetPos;
    [SerializeField] float fallSpeed = 1f;
    public bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, targetPos, fallSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PresentBox")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            flag = true;
        }
    }
}
