using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 角色物理框
    /// </summary>
    private Rigidbody2D playerRigidbody2D;
    /// <summary>
    /// 最大水平速度
    /// </summary>
    private float maxVelocityX = 10;
    /// <summary>
    /// 水平推力
    /// </summary>
    public float forceX;
    /// <summary>
    /// 垂直推力
    /// </summary>
    public float forceY;
    /// <summary>
    /// 角色生命值
    /// </summary>
    public static int hp;

    void Start()
    {
        hp = 10;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Mathf.Abs(playerRigidbody2D.velocity.x) < maxVelocityX)
            {
                playerRigidbody2D.AddForce(new Vector2(forceX * -1, 0));
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(playerRigidbody2D.velocity.x) < maxVelocityX)
            {
                playerRigidbody2D.AddForce(new Vector2(forceX, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody2D.AddForce(new Vector2(0, forceY));
        }
    }
}
