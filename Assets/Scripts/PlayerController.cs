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
    private float maxVelocityX = 4.0f;
    /// <summary>
    /// 最大垂直速度
    /// </summary>
    private float maxVelocityY = 10.0f;
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
        // 水平移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(maxVelocityX * -1, playerRigidbody2D.velocity.y);
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(maxVelocityX, playerRigidbody2D.velocity.y);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && playerRigidbody2D.velocity.y == 0)
        {
            //playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 5);
            playerRigidbody2D.AddForce(new Vector2(0, forceY));
        }
        else if (Mathf.Abs(playerRigidbody2D.velocity.y) > maxVelocityY)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, maxVelocityY * -1);
        }
    }

    /// <summary>
    /// 當角色受到傷害
    /// </summary>
    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }

        GameManageController.Instance.RenderHealthUI();

        StartCoroutine(nameof(GetInvisible));
    }

    /// <summary>
    /// 進入無敵狀態
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetInvisible()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        boxCollider.enabled = true;
    }
}
