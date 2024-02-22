//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    //public InputAction flapping;
    public float movVal;
    public bool flapped = false;

    public MoveComponent mover;
    /*
    void OnEnable()
    {
        flapping.Enable();
    }

    void OnDisable()
    {
        flapping.Disable();
    }
    */
    void Update()
    {
        if (Input.GetButtonDown("Jump")) movVal = 1f;
        else movVal = 0f;
        Vector2 movResult = mover.Move(Vector2.up * movVal);
        transform.position += new Vector3(0f, movResult.y, 0f);
    }
}
