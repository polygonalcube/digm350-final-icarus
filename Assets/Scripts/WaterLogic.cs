using UnityEngine;

public class WaterLogic : MonoBehaviour
{
    public float freq = 1f;
    public float intensity = 0.5f;
    public float offX = 0f;
    public float offY = 5.5f;

    void Update()
    {
        transform.localPosition = new Vector3(0f, Mathf.Sin(Time.time * freq + offX) * intensity + offY, 0f);
    }
}
