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

    public SpawningComponent arrowSpawner;
    public SpawningComponent cloudSpawner;
    
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
        for (int i = 0; i < 100; i++)
        {
            GameObject newCloud = cloudSpawner.Spawn(new Vector3(((float)i * 4f) - 4f, Random.Range(-1.5f, 1f), 0f));
        }
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
