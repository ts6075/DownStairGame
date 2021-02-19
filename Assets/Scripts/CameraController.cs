using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 下降速度
    /// </summary>
    public float downSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -downSpeed * Time.deltaTime, 0);
    }
}
