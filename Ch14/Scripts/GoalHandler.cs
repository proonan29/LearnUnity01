using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalHandler : MonoBehaviour
{
    public Text success;

    // Start is called before the first frame update
    void Start()
    {
        if (success != null)
        {
            success.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision?.transform.tag == "Player")
        {
            if (success != null)
            {
                success.gameObject.SetActive(true);
            }
        }
    }
}
