using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject canvasPopup;
    public GameObject buttonOpen;

    // Start is called before the first frame update
    void Start()
    {
        canvasPopup.SetActive(false);
        buttonOpen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopup()
    {
        canvasPopup.SetActive(false);
        buttonOpen.SetActive(true);
    }

    public void OpenPopup()
    {
        canvasPopup.SetActive(true);
        buttonOpen.SetActive(false);
    }
}
