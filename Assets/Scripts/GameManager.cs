//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public enum GameState
    {
        Title,
        Await,
        Game,
        Death,
        Win
    }
    public GameState gameState = GameState.Title;

    public SpawningComponent[] arrowSpawners;
    float[] arrowDelays;
    public float[] arrowDelaySets;
    public float[] arrowDelayMins;
    float[] origDelays;
    public float[] delayDecs;

    public SpawningComponent cloudSpawner;
    GameObject[] clouds = new GameObject[300];

    Transform cam;
    
    void Awake() // Allows for Singleton.
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            GameObject newCloud = cloudSpawner.Spawn(new Vector3(((float)i * 4f) - 6f, Random.Range(-2.5f, 0.5f), 0f));
            clouds[i] = newCloud;
        }
        clouds[0].transform.position = new Vector3(clouds[0].transform.position.x, 4.5f, 0f);

        cam = GameObject.Find("Main Camera").transform;

        arrowDelays = new float[arrowSpawners.Length];
        origDelays = new float[arrowSpawners.Length];
        for (int i = 0; i < arrowSpawners.Length; i++)
        {
            origDelays[i] = arrowDelaySets[i];
            arrowDelays[i] = arrowDelaySets[i];
        }
    }

    void Update()
    {
        for (int i = 0; i < arrowSpawners.Length; i++)
        {
            if (gameState == GameState.Game)
            {
                arrowDelays[i] -= Time.deltaTime;
                if (arrowDelays[i] <= 0f)
                {
                    GameObject newArrow = arrowSpawners[i].Spawn(new Vector3(Random.Range(1.5f, 9.5f) + cam.position.x, -5.5f, 0f));
                    newArrow.GetComponent<ArrowLogic>().mover.jumpHgt = Random.Range(1f, 6f);
                    arrowDelaySets[i] -= delayDecs[i];
                    if (arrowDelaySets[i] < arrowDelayMins[i]) arrowDelaySets[i] = arrowDelayMins[i];
                    arrowDelays[i] = arrowDelaySets[i];
                }
            }
            else
            {
                arrowDelaySets[i] = origDelays[i];
                arrowDelays[i] = arrowDelaySets[i];
            }
        }
    }

    public void RandomizeClouds()
    {
        for (int i = 0; i < 300; i++) clouds[i].transform.position = new Vector3(clouds[i].transform.position.x, Random.Range(-2.5f, 0.5f), 0f);
        clouds[0].transform.position = new Vector3(clouds[0].transform.position.x, 4.5f, 0f);
    }

    public int Sign(float num)
    {
        if (num < 0)
        {
            return -1;
        }
        else if (num > 0)
        {
            return 1;
        }
        return 0;
    }

    public GameObject FindPlayer()
    {
        return GameObject.Find("Player");
    }

    public PlayerLogic FindPlayerScript()
    {
        return GameObject.Find("Player").GetComponent<PlayerLogic>();
    }
}
