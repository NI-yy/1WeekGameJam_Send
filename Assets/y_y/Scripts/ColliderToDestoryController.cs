using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToDestoryController : MonoBehaviour
{
    public bool is_flaying = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(is_flaying))
        {
            if(collision.gameObject.tag == "PresentBox")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
