using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private int score_box = 0;
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
        var obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            Debug.Log("Goal");
        }
        else if (obj.tag == "PresentBox")
        {
            Destroy(obj);
            score_box++;
            Debug.Log("Score: " + score_box);
        }
    }
}
