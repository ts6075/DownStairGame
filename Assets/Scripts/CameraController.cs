using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 下降速度
    /// </summary>
    public float downSpeed;

    private void FixedUpdate()
    {
        transform.Translate(0, -downSpeed * Time.deltaTime, 0);
    }
}
