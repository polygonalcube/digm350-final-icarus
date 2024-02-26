//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public HPComponent hp;
    public MoveComponent mover;
    public MoveComponent moverDeath;

    public float movVal;
    public float movValDeath = 1f;
    
    void Update()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            movValDeath = 1f;
            Movement();
        }
        if (GameManager.gm.gameState == GameManager.GameState.Death)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Game)
        {
            SunDamage();
        }
    }

    void Movement()
    {
        movVal = (Input.GetButtonDown("Jump")) ? 1f : 0f;
        Vector2 movResult = mover.Move(Vector2.up * movVal);
        transform.position += (Vector3)movResult;
    }

    void SunDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up);

        if (hit.collider == null)
        {
            hp.health -= 1;
        }
    }

    void Die()
    {
        Vector2 movResult = moverDeath.Move(Vector2.up * movValDeath);
        transform.position += (Vector3)movResult;
        movValDeath = 0f;
    }
}
