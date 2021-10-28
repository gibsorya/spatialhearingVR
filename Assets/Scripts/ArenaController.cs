using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject balloon, parentObject;
    [SerializeField]
    private float radius, balloonHeight;

    public int numOfBalloons;

    private GameObject[] balloons = new GameObject[360];


    void Start()
    {
        spawnBalloons(radius);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playSpound();
        }
    }


    public void spawnBalloons(float radius)
    {
        for (int i = 0; i < numOfBalloons; i++)
        {
            Vector3 center = transform.position;
            Vector3 pos;

            pos.x = center.x + radius * Mathf.Sin(i * Mathf.Deg2Rad);
            pos.y = center.y + balloonHeight;
            pos.z = center.z + radius * Mathf.Cos(i * Mathf.Deg2Rad);
            GameObject go = Instantiate(balloon, pos, Quaternion.identity);
            go.name = "Balloon " + (i + 1);
            balloons[i] = go;
            go.transform.SetParent(parentObject.transform);

        }
    }

    public void playSpound()
    {
        int randomNum = Random.Range(0, 359);
        balloons[0].GetComponent<AudioSource>().Play();


    }
}
