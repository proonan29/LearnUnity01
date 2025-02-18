using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public List<GameObject> prefabObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int idx = Random.Range(0, prefabObj.Count);
            Instantiate(prefabObj[idx]);
        }
    }
}
