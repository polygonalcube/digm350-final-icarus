//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] float scrollSpd = 1f;

    void Update()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            transform.position += Vector3.right * scrollSpd * Time.deltaTime;
        }
    }
}
