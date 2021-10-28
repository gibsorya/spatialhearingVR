using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcomescreen : MonoBehaviour
{
    public GameObject play;
    public GameObject setting;
    public GameObject gamename;
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject quit;
    public GameObject back;
    public GameObject image;
    public GameObject Slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set()
    {
        play.SetActive(false);
        setting.SetActive(false);
        pattern1.SetActive(false);
        pattern2.SetActive(false);
        quit.SetActive(false);
        gamename.SetActive(false);
        image.SetActive(true);
        Slider.SetActive(true);
        back.SetActive(true);
    }
    public void Play()
    {
        //跳转界面，取消下方代码注释，在“”内添加相应的场景名字
        //SceneManager.GetSceneByName("");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        play.SetActive(true);
        setting.SetActive(true);
        pattern2.SetActive(true);
        quit.SetActive(true);
        gamename.SetActive(true);
        image.SetActive(false);
        Slider.SetActive(false);
        back.SetActive(false);
    }
    public void Pattern1()
    {  
        pattern1.SetActive(false);
        pattern2.SetActive(true);
    }
    public void Pattern2()
    {
        pattern1.SetActive(true);
        pattern2.SetActive(false);
    }
}
