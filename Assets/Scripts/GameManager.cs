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

    public SpawningComponent[] cloudSpawners;
    GameObject[] clouds = new GameObject[300];

    Transform cam;

    //public bool isBuildA = true;
    
    void Awake() // Allows for Singleton.
    {
        if (gm != null && gm != this) Destroy(this.gameObject);
        else gm = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            if (i == 0) clouds[i] = cloudSpawners[0].Spawn(new Vector3(((float)i * 4f) - 6f, 4.5f, 0f));
            else if (i < 85) clouds[i] = cloudSpawners[(int)Mathf.Round(Random.Range(0f, 0.75f))].Spawn(new Vector3(((float)i * 4f) - 6f, Random.Range(-0.5f, 2.5f), 0f));
            else if (i < 170) clouds[i] = cloudSpawners[(int)Mathf.Round(Random.Range(-0.4f, 2.4f))].Spawn(new Vector3(((float)i * 4f) - 6f, Random.Range(-1.5f, 1.5f), 0f));
            else clouds[i] = cloudSpawners[(int)Mathf.Round(Random.Range(1.25f, 2f))].Spawn(new Vector3(((float)i * 4f) - 6f, Random.Range(-2.5f, 0.5f), 0f));
        }
        //clouds[0].transform.position = new Vector3(clouds[0].transform.position.x, 4.5f, 0f);

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
                if (arrowDelays[i] <= 0f && GameObject.Find("UI").GetComponent<UI>().attemptTime < ((cam.GetComponent<CameraLogic>().secondsToWin / 7f) * 6f))
                {
                    GameObject newArrow = arrowSpawners[i].Spawn(new Vector3(Random.Range(1.5f, 9.5f) + cam.position.x, -5.5f, 0f), destroyTimer: 5f);
                    newArrow.GetComponent<ArrowLogic>().mover.jumpHgt = Random.Range(1f, 9f);
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
        // Build switching (for beta testing).
        /*
        if (Input.GetKeyDown("a"))
        {
            isBuildA = true;
        }
        else if (Input.GetKeyDown("b"))
        {
            isBuildA = false;
        }*/
    }

    public void RandomizeClouds()
    {
        for (int i = 0; i < 300; i++)
        {
            if (i == 0) clouds[i].transform.position = new Vector3(clouds[0].transform.position.x, 4.5f, 0f);
            else if (i < 85) clouds[i].transform.position = new Vector3(clouds[i].transform.position.x, Random.Range(-0.5f, 2.5f), 0f);
            else if (i < 170) clouds[i].transform.position = new Vector3(clouds[i].transform.position.x, Random.Range(-1.5f, 1.5f), 0f);
            else clouds[i].transform.position = new Vector3(clouds[i].transform.position.x, Random.Range(-2.5f, 0.5f), 0f);
        }
    }

    public int Sign(float num)
    {
        if (num < 0) return -1;
        else if (num > 0) return 1;
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
