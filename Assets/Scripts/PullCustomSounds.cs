                        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PullCustomSounds : MonoBehaviour
{
    public ArenaController arenaController;
    
    public ArrayList files = new ArrayList();
    string currentDirectory = Directory.GetCurrentDirectory();
    string fileName, itdSub, ildSub;
    int itdPosition, ildPosition;
    int size = 0;
    int length;
    void Start()
    {
        arenaController = GameObject.Find("Arena").gameObject.GetComponent<ArenaController>();
        
        size = Directory.GetFiles(currentDirectory + @"\soundFiles", "*.wav").Length;
        string[] soundFiles = new string[size];
        soundFiles = Directory.GetFiles(currentDirectory + @"\soundFiles", "*.wav");
        for (int i = 0; i < size; i++)
        {
            files.Add(soundFiles[i]);
        }
    }

    public void PlaySpatialSound()
    {
        int randomNum = Random.Range(0, files.Count);
        Debug.Log(files[randomNum].ToString());
        Application.OpenURL(files[randomNum].ToString());
        SetData(randomNum);
        files.RemoveAt(randomNum);
            
    }

    public void SetData(int num)
    {
        fileName = files[num].ToString();

        itdPosition = fileName.LastIndexOf("ITD") + "ITD".Length;
        length = fileName.IndexOf("_") - itdPosition;
        itdSub = fileName.Substring(itdPosition, length);
        arenaController.itdData.Add(itdSub);

        ildPosition = fileName.LastIndexOf("ILD") + "ILD".Length;
        length = fileName.IndexOf(".") - ildPosition;
        ildSub = fileName.Substring(ildPosition, length);
        arenaController.ildData.Add(ildSub);
    }
}
