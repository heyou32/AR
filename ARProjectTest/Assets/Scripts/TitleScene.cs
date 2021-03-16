using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    #region #╫л╠шео
    public static TitleScene instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    AudioSource startSFX;
    AudioSource btnSFX;
    public GameObject button;
    public GameObject[] carPrefabs;
    public Color[] colors;
    public Transform point;
    GameObject seleted;
    bool state = true;
    bool ready;
    private void Start()
    {
        startSFX = GetComponent<AudioSource>();
        btnSFX = button.GetComponent<AudioSource>();
    }
    private void Update()
    {
        //print("model = " + PlayerPrefs.GetInt("Model"));
        //print("color = " + PlayerPrefs.GetInt("COLOR"));
        if (state)
        {
            GameObject car = GameObject.FindWithTag("Model");
            car.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 50);
        }
        if (ready)
        {
            seleted.transform.position = Vector3.MoveTowards(seleted.transform.position, new Vector3(0, 0, -10), 50 * Time.deltaTime);
        }
    }
    #region Property
    int modelNum;
    public int Model
    {
        get { return modelNum; }
        set
        {
            modelNum = value;
            PlayerPrefs.SetInt("Model", modelNum);
        }
    }
    int colorNum;
    public int COLOR
    {
        get { return colorNum; }
        set
        {
            colorNum = value;
            PlayerPrefs.SetInt("COLOR", colorNum);
        }
    }
    #endregion
    #region Car Model
    void Car(int a)
    {
        btnSFX.Play();
        if (GameObject.FindWithTag("Model"))
            Destroy(GameObject.FindWithTag("Model"));
        GameObject car = Instantiate(carPrefabs[a - 1]);
        car.transform.position = point.position;
        car.transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public void Car1()
    {
        Model = 0;
        Car(1);
    }
    public void Car2()
    {
        Model = 1;
        Car(2);
    }
    public void Car3()
    {
        Model = 2;
        Car(3);
    }
    #endregion
    #region Color
    public void ColorChange(Color value)
    {
        btnSFX.Play();
        GameObject[] mats = GameObject.FindGameObjectsWithTag("Paint");
        if (mats != null)
        {
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i].GetComponent<MeshRenderer>().material.color = value;
            }
        }
    }

    public void Red()
    {
        COLOR = 0;
        ColorChange(colors[0]);
    }
    public void Blue()
    {
        COLOR = 1;
        ColorChange(colors[1]);
    }
    public void White()
    {
        COLOR = 2;
        ColorChange(colors[2]);
    }
    public void Black()
    {
        COLOR = 3;
        ColorChange(colors[3]);
    }
    public void Green()
    {
        COLOR = 4;
        ColorChange(colors[4]);
    }
    public void Yellow()
    {
        COLOR = 5;
        ColorChange(colors[5]);
    }
    #endregion
    #region Start Button
    public void StartBtn()
    {
        startSFX.Play();
        state = false;
        seleted = GameObject.FindWithTag("Model");
        seleted.transform.eulerAngles = new Vector3(0, 180, 0);
        Invoke("CarMoving", 0.4f);
        Invoke("SceneChange", 1.5f);
    }
    void CarMoving()
    {
        ready = true;
    }
    void SceneChange()
    {
        //DontDestroyOnLoad(seleted);
        SceneManager.LoadScene("Play Scene");
    }
    #endregion
}
