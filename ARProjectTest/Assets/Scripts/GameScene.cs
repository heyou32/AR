using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    public GameObject[] models;
    public Color[] colors;
    public GameObject cube;
    ARRaycastManager arrm;
    GameObject player;
    GameObject playerPrefab;
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
        //GameObject ex = Instantiate(playerPrefab);
       quitUI.SetActive(false);
    }
    void Update()
    {
        if (gameObject.CompareTag("Paint"))
            print(0);
       bool click=false;
        if (Input.GetMouseButtonDown(0) &&click == false)
        {
            click = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue))
            {
                //GameObject p = Instantiate(player);
                player = Instantiate(playerPrefab);
                player.transform.position = hitinfo.point;
                player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                TitleScene.instance.ColorChange(colors[PlayerPrefs.GetInt("COLOR")]);
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
            AudioListener.volume=1;
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
}
