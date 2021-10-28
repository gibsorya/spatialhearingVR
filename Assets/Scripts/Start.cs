    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public bool spatialScene = false;
    public GameObject mainMenu, settingsMenu, arenaController;
    //public ArenaController arenaController;
    public Text subject;
    public Text experiment;
    public Text condition;
    public Text run;
    public string fileName;


    public void playButtonPress()
    {
        if (spatialScene){
            fileName = subject.text + "_" + experiment.text + "_" + condition.text + "_" + run.text + ".txt";
            arenaController.GetComponent<ArenaController>().fileName = fileName;
            StartCoroutine(LoadMScene());

        } else
        {
            fileName = subject.text + "_" + experiment.text + "_" + condition.text + "_" + run.text + ".txt";
            arenaController.GetComponent<ArenaController>().fileName = fileName;
            StartCoroutine(LoadArenaScene());
        }
        
    }

    public void setSpacial(bool value)
    {
        if (value)
        {
            spatialScene = true;
        } else
        {
            spatialScene = false;
        }
    }

    public void backButtonPressed()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void settingsButtonPressed()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void quitButtonPressed()
    {
        Application.Quit();
    }

    IEnumerator LoadMScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MScene", LoadSceneMode.Additive);
        DontDestroyOnLoad(arenaController);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(arenaController.gameObject, SceneManager.GetSceneByName("MScene"));
        arenaController.GetComponent<ArenaController>().spawnBalloons(arenaController.GetComponent<ArenaController>().radius);
        SceneManager.UnloadSceneAsync(currentScene);
    }

    IEnumerator LoadArenaScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MScene", LoadSceneMode.Additive);
        DontDestroyOnLoad(arenaController);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(arenaController.gameObject, SceneManager.GetSceneByName("ArenaScene"));
        arenaController.GetComponent<ArenaController>().spawnBalloons(arenaController.GetComponent<ArenaController>().radius);
        SceneManager.UnloadSceneAsync(currentScene);
    }

}
