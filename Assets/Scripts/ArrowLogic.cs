using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    public MoveComponent mover;

    public Vector2 dir = new Vector2();

    void Start()
    {
        Vector2 movResult = mover.Move(dir);
        transform.position += new Vector3(movResult.x, movResult.y, 0f);
        dir = new Vector2(dir.x, 0f);
    }

    void Update()
    {
        Vector2 movResult = mover.Move(dir);
        transform.position += new Vector3(movResult.x, movResult.y, 0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Vector2.Angle(movResult));
    }

    /*Survey
    On a scale of 1 (worst) to 7 (best), how fun would you say the game was?
    - 1  - 2  - 3  - 4  - 5  - 6  - 7
    Did the game feel like the story of Icarus?
    - Yes  - No
    Did you reach the end island?
    - Yes  - No  - Wait, there's an island?!
    If played, which Icarus game was cooler?
    - Ours  - Theirs
    */
}
