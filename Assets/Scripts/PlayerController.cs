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
    private float maxVelocityX = 3.0f;
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
            playerRigidbody2D.velocity = new Vector2(maxVelocityX * -1, playerRigidbody2D.velocity.y);
            //if (Mathf.Abs(playerRigidbody2D.velocity.x) < maxVelocityX)
            //{
            //    playerRigidbody2D.AddForce(new Vector2(forceX * -1, 0));
            //}
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(maxVelocityX, playerRigidbody2D.velocity.y);
            //if (Mathf.Abs(playerRigidbody2D.velocity.x) < maxVelocityX)
            //{
            //    playerRigidbody2D.AddForce(new Vector2(forceX, 0));
            //}
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody2D.AddForce(new Vector2(0, forceY));
        }

        if (playerRigidbody2D.velocity.x < 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
