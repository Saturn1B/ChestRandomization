using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(11);
        var i = Random.value;
        var j = Random.value;
        Debug.Log(i + " | " + j);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
