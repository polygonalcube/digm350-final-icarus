using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    public MoveComponent mover;

    public Vector2 dir = new Vector2();

    void Start()
    {
        transform.position += (Vector3)mover.Move(dir);
        dir = new Vector2(dir.x, 0f);
    }

    void Update()
    {
        Vector2 movResult = mover.Move(dir);
        transform.position += (Vector3)movResult;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan(movResult.y/movResult.x) * Mathf.Rad2Deg);
    }
}
