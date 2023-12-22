using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigPresentBoxController : MonoBehaviour
{
    [SerializeField] float X_amount = 1f;
    [SerializeField] float Y_amount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if(obj.tag == "PresentBox")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1f * X_amount, 1f * Y_amount), ForceMode2D.Impulse);
        }
    }
}
