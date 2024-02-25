//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float accel;
    public float decel;
    public float gravity;
    public float jumpHgt;
	public Vector2 maxSpeed;

    public Vector2 speed;

    // A version of Mathf.Sign() that can return 0;
    int Sign(float num)
    {
        return (num == 0) ? ((num < 0) ? -1 : 1) : 0;
    }
    
    public float Accelerate(float speedVar, float axis)
    {
        return speedVar + (accel * axis * Time.deltaTime);
    }

    public float Decelerate(float speedVar)
    {
        speedVar += decel * (float)Sign(-speedVar) * Time.deltaTime;
	    if (Mathf.Abs(speedVar) <= decel) speedVar = 0f;
		return speedVar;
    }

    public float Cap(float speedVar, float speedCap, float magni)
    {
        return (Mathf.Abs(speedVar) > (speedCap * Mathf.Abs(magni))) ? (speedCap * magni) : speedVar;
    }

    public Vector2 Move(Vector2 moveDir)
    {
        // acceleration & deceleration
		speed.x = (Mathf.Abs(moveDir.x) != 0f) ? Accelerate(speed.x, moveDir.x) : Decelerate(speed.x);
        if (gravity != 0f)
        {
            speed.y += gravity * Time.deltaTime;
        }
        else
        {
            speed.y = (Mathf.Abs(moveDir.y) != 0f) ? Accelerate(speed.y, moveDir.y) : Decelerate(speed.y);
        }
        if (moveDir.y >= 0.5f && gravity != 0f && jumpHgt != 0f) speed.y = Mathf.Abs(Mathf.Sqrt(jumpHgt * -2f * gravity));

        speed.x = Cap(speed.x, maxSpeed.x, moveDir.x);
        if (gravity != 0f)
        {
            speed.y = Cap(speed.y, maxSpeed.y, -1f);
        }
        else
        {
            speed.y = Cap(speed.y, maxSpeed.y, moveDir.y);
        }

		return new Vector2(speed.x, speed.y) * Time.deltaTime;
    }
}
