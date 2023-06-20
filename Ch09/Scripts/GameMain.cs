using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public List<GameObject> prefabObj;

    // Start is called before the first frame update
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
