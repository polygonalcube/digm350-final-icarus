using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DaedalusLogic : MonoBehaviour
{
    public MoveComponent mover;
    public float movVal;
    
    void Update()
    {
        if (GameManager.gm.gameState != GameManager.GameState.Title)
        {
            Movement();
        }
        else movVal = 1f;
    }

    void Movement()
    {
        Vector2 movResult = mover.Move(new Vector2(1f, movVal));
        transform.position += (Vector3)movResult;
        movVal = (transform.position.y < -3f) ? 1f : 0f;
    }
}
