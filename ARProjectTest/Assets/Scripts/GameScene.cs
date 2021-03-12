using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    public GameObject[] models;
    public Color[] colors;
    public GameObject cube;
    ARRaycastManager arrm;
    GameObject player;
    private void Start()
    {
        //print("model = "+PlayerPrefs.GetInt("Model"));
        //print("color = "+PlayerPrefs.GetInt("COLOR"));
        arrm = GetComponent<ARRaycastManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue))
            {
                //GameObject p = Instantiate(player);
                player = Instantiate(models[PlayerPrefs.GetInt("Model")]);
                player.transform.position = hitinfo.point;
                player.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
                player.GetComponent<MeshRenderer>().material.color = colors[PlayerPrefs.GetInt("COLOR")];
            }
        }
    }
    public GameObject d;
    public GameObject r;
    bool onD = true;
    public void GearChange()
    {
        onD = !onD;
        if (onD)
        {
            d.SetActive(true);
            r.SetActive(false);
        }
        else
        {
            r.SetActive(true);
            d.SetActive(false);
        }
    }
}
