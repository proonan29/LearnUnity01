using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloWorld : MonoBehaviour
{
    const string hw = "Hello World!";
    // Start is called before the first frame update
    void Start()
    {
        Text message = GetComponent<Text>();
        message.text = hw;
        Debug.Log(hw);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
