using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody2D;

    /// <summary>
    /// 水平推力
    /// </summary>
    public float forceX;

    /// <summary>
    /// 垂直推力
    /// </summary>
    public float forceY;

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.AddForce(new Vector2(forceX * -1, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.AddForce(new Vector2(forceX, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody2D.AddForce(new Vector2(0, forceY));
        }
    }
}
