using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;


public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private float sightLength;
    // Start is called before the first frame update
    public SteamVR_Action_Boolean BooleanAction;
    public SteamVR_ActionSet ActionSet;
    [SerializeField]
    private bool triggerPressed = false, hasSoundPlayed = false, isBoxActive = true, hasRoutineStarted = false, hasLastBalloonPopped = false;
    [SerializeField]
    private GameObject balloon, homeBox, poppedBalloon, finalScreenObject;
    public ArenaController arenaController;
    public PullCustomSounds customSounds;
    public Transform head;
    public SteamVR_Input_Sources handType;
    private float currentTime, maxTime = 3;
    public string balloonName;
    void Awake()
    {
        arenaController = GameObject.Find("Arena").gameObject.GetComponent<ArenaController>();
        BooleanAction = SteamVR_Actions._default.GrabPinch;
        BooleanAction.AddOnStateDownListener(TriggerUp, handType);
    }

    private void Start()
    {
        arenaController.spawnBalloons(arenaController.radius);
        ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);

    }

    // Update is called once per frame
    void Update()
    {
        if (customSounds.files.Count > 0)
        {


            RaycastHit ray;
            Ray rayDirection = new Ray(transform.position, transform.forward);
            if (isBoxActive)
            {
                if (Physics.Raycast(rayDirection, out ray, sightLength))
                {
                    if (ray.collider.tag == "HomeBox")
                    {
                        currentTime += 1 * Time.deltaTime;

                        if (currentTime >= maxTime)
                        {
                            homeBox.SetActive(false);
                            isBoxActive = false;
                            currentTime = 0;
                        }

                    }
                }
            }
            else if (!isBoxActive)
            {
                if (!hasSoundPlayed)
                {
                    if (SceneManager.GetActiveScene().name == "ArenaScene")
                    {
                        arenaController.playSound();
                        hasSoundPlayed = true;
                    }
                    else
                    {
                        customSounds.PlaySpatialSound();
                        hasSoundPlayed = true;
                    }
                }
                if (triggerPressed) //change this when using new VR controller
                {
                    triggerPressed = false;
                    Debug.Log("Controller pressed!");
                    //if (Physics.Raycast(rayDirection, out ray, sightLength))
                    //{
                    //    if (ray.collider.tag == "Balloon")
                    //    {
                    //        Debug.Log("Balloon hit!");
                    //        homeBox.SetActive(true);
                    //        isBoxActive = true;
                    //        balloonName = ray.collider.gameObject.name;
                    //        ray.collider.gameObject.SetActive(false);

                    //        ray.collider.gameObject.GetComponent<Balloon>().ApplyDamage();
                    //        SendData(balloonName, GetCurrentFrameData());
                            
                    //        poppedBalloon = ray.collider.gameObject;
                    //        //GameObject.Find(balloonName).SetActive(false);
                    //        hasSoundPlayed = false;
                    //        arenaController.OnClose();
                    //        if(customSounds.files.Count == 0)
                    //        {
                    //            hasLastBalloonPopped = true;    
                    //        }
                    //    }
                    //}
                    Debug.DrawRay(transform.position, transform.forward, Color.black, 1);
                }
            }
        } else if (hasLastBalloonPopped == true)
        {
            Debug.Log("Last balloon popped");
                if (hasRoutineStarted == false)
                {
                    StartCoroutine(waitBeforeSceneSwitch());
                    hasRoutineStarted = true;
                }
           
            
            
        }
        
    }

    public void endGame()
    {
        //arenaController.FloatingBallons();

        bool timerDone = false;
        float maxTime = 15;
        float currentTime = 0;
        while (!timerDone)
        {
            currentTime += 1 * Time.deltaTime;
            Debug.Log("Count: " + currentTime);
            if (currentTime >= maxTime)
            {
                timerDone = true;
            }
        }

        //SceneManager.LoadScene(0);
    }

    public bool isHomeBoxActive()
    {
        if (homeBox.gameObject.activeSelf)
        {
            return true;
        } else
        {
            return false;
        }
    }

    FrameData GetCurrentFrameData()
    {
        var frameData = new FrameData();
        frameData.headPosition = head.localPosition; // relative to body.
        frameData.headRotation = head.localRotation.eulerAngles;
        return frameData;
    }

    public void SendData(string balloonPopped, FrameData headPosition)
    {
        arenaController.balloonPlayedData.Add(balloonPopped);
        arenaController.balloonPoppedData.Add(balloonPopped);
        if (headPosition.headRotation.y > 180)
        {
            headPosition.headRotation.y = -1*(180+(180 - headPosition.headRotation.y));
        }
        arenaController.headPositionData.Add(headPosition);
    }

    IEnumerator waitBeforeSceneSwitch()
    {
        finalScreenObject.SetActive(true);
        yield return new WaitForSecondsRealtime(15);
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ThisScene", LoadSceneMode.Additive);
        DontDestroyOnLoad(arenaController);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(arenaController.gameObject, SceneManager.GetSceneByName("ThisScene"));
        arenaController.GetComponent<ArenaController>().spawnBalloons(arenaController.GetComponent<ArenaController>().radius);
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        RaycastHit ray;
        Ray rayDirection = new Ray(transform.position, transform.forward);
        triggerPressed = true;
        Debug.Log("Trigger is up");
        if (Physics.Raycast(rayDirection, out ray, sightLength))
        {
            if (ray.collider.tag == "Balloon")
            {
                Debug.Log("Balloon hit!");
                homeBox.SetActive(true);
                isBoxActive = true;
                balloonName = ray.collider.gameObject.name;
                ray.collider.gameObject.SetActive(false);

                ray.collider.gameObject.GetComponent<Balloon>().ApplyDamage();
                SendData(balloonName, GetCurrentFrameData());

                poppedBalloon = ray.collider.gameObject;
                //GameObject.Find(balloonName).SetActive(false);
                hasSoundPlayed = false;
                arenaController.OnClose();
                if (customSounds.files.Count == 0)
                {
                    hasLastBalloonPopped = true;
                }
            }
        }

    }
}
