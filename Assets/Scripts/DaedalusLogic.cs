using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DaedalusLogic : MonoBehaviour
{
    public MoveComponent mover;
    public float movVal;

    Vector3 globalStart;
    Vector3 localStart;

    void Start()
    {
        globalStart = transform.position;
        localStart = transform.localPosition;
    }
    
    void Update()
    {
        /*if (GameManager.gm.gameState != GameManager.GameState.Title)
        {
            Movement();
        }
        else movVal = 1f;*/
        if (GameManager.gm.gameState == GameManager.GameState.Title)
        {
            transform.position = globalStart;
            mover.speed = Vector2.zero;
            movVal = 1f;
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Await)
        {
            Movement();
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            Movement();
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Death)
        {
            Movement();
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Win)
        {
            
        }
    }

    void Movement()
    {
        Vector2 movResult = mover.Move(new Vector2(1f, movVal));
        transform.position += (Vector3)movResult;
        movVal = (transform.position.y < -3f) ? 1f : 0f;
    }
}
