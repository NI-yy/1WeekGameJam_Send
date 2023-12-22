using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadLineController : MonoBehaviour
{
    [SerializeField] GameObject GameOverUIManager;

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
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            GameOverUIManager.GetComponent<GameOverManager_yy>().ActivateGameOverUI();
        }
    }
}
