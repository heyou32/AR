using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject cubePrefab;
    void Start()
    {
        GameObject cube = GameObject.Find("cube(Clone)");
        if (cube = null)
        {
            cube = Instantiate(cubePrefab);
            cube.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
