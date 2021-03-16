using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    public GameObject[] models;
    public Color[] colors;
    ARRaycastManager arrm;
    GameObject player;
    GameObject playerPrefab;
    public GameObject planePrefab;
    public GameObject menuUI;
    public GameObject quitUI;
    bool menuOn;
    bool mute;
    private void Start()
    {
        //print("model = "+PlayerPrefs.GetInt("Model"));
        //print("color = "+PlayerPrefs.GetInt("COLOR"));
        arrm = GetComponent<ARRaycastManager>();
        playerPrefab = models[PlayerPrefs.GetInt("Model")];
        //player = models[PlayerPrefs.GetInt("Model")];
        quitUI.SetActive(false);
    }
    //bool click;
    Ray ray = new Ray();
    void Update()
    {
        GameObject active = GameObject.FindGameObjectWithTag("Model");
        if (Input.GetMouseButtonDown(0) && active == null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue))
            {
                print("d");
                player = Instantiate(playerPrefab);
                GameObject plane = Instantiate(planePrefab);
                player.transform.position = hitinfo.point;
                plane.transform.position = hitinfo.point;
                player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                ColorChange(colors[PlayerPrefs.GetInt("COLOR")]);
            }
        }
        void ColorChange(Color value)
        {
            GameObject[] mats = GameObject.FindGameObjectsWithTag("Paint");
            if (mats != null)
            {
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].GetComponent<MeshRenderer>().material.color = value;
                }
            }
        }

        if (menuOn)
        {
            menuUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(false);
        }

        if (mute)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }
    #region Gear
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
    #endregion
    #region MenuUI
    public void Menu()
    {
        menuOn = !menuOn;
    }
    public void Mute()
    {
        mute = !mute;
    }
    public void GotoTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void Quit()
    {
        quitUI.SetActive(true);
    }
    public void YesQuit()
    {
        Application.Quit();
    }
    public void NoQuit()
    {
        quitUI.SetActive(false);
    }
    #endregion
}
