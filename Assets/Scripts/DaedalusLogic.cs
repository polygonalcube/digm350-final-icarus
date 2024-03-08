using System.Collections;
using UnityEngine;

public class DaedalusLogic : MonoBehaviour
{
    public MoveComponent mover;
    public float movVal;

    Vector3 globalStart;
    Vector3 localStart;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;

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
            Animation();
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            Movement();
            Animation();
        }
        else if (GameManager.gm.gameState == GameManager.GameState.Death)
        {
            Movement();
            Animation();
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

    void Animation()
    {
        sr.sprite = (mover.speed.y > 0f) ? sprites[1] : sprites[0];
    }
}
