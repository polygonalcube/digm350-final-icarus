using System.Collections;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] float scrollSpd = 1f;
    Vector3 refVelocity = Vector3.zero;

    public float secondsToWin = 300f;
    Transform island;

    void Start()
    {
        island = GameObject.Find("Island").transform;
        if (island != null) island.position = new Vector3(scrollSpd * secondsToWin, island.position.y, 0f);
    }

    void Update()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Title)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0f, 0f, -10f), ref refVelocity, 0.5f);
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Await)
        {
            transform.position = new Vector3(0f, 0f, -10f);
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            transform.position += Vector3.right * scrollSpd * Time.deltaTime;
        }
    }
}
