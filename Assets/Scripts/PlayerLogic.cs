//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    public HPComponent hp;
    public MoveComponent mover;

    public float movVal;
    
    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        SunDamage();
    }

    void Movement()
    {
        movVal = (Input.GetButtonDown("Jump")) ? 1f : 0f;
        Vector2 movResult = mover.Move(Vector2.up * movVal);
        transform.position += new Vector3(movResult.x, movResult.y, 0f);
    }

    void SunDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up);

        if (hit.collider == null)
        {
            hp.health -= 1;
        }
    }
}
