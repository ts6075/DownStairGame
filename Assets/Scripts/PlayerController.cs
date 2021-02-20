using System.Collections;
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
    private readonly float maxVelocityX = 4.0f;
    /// <summary>
    /// 最小垂直速度
    /// </summary>
    private readonly float minVelocityY = 3.0f;
    /// <summary>
    /// 最大垂直速度
    /// </summary>
    private readonly float maxVelocityY = 30.0f;
    /// <summary>
    /// 角色最大生命值
    /// </summary>
    public int maxHp;
    /// <summary>
    /// 角色生命值
    /// </summary>
    public int hp;

    void Start()
    {
        hp = maxHp;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 判斷生命值
        if (hp <= 0)
        {
            GameManageController.Instance.GameOver();
        }

        // 水平移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(maxVelocityX * -1, playerRigidbody2D.velocity.y);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            gameObject.GetComponent<Animator>().Play("SkeletonWalkAnimation");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(maxVelocityX, playerRigidbody2D.velocity.y);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.GetComponent<Animator>().Play("SkeletonWalkAnimation");
        }
        else
        {
            playerRigidbody2D.velocity *= new Vector2(0, 1);
            gameObject.GetComponent<Animator>().Play("SkeletonStandAnimation");
        }

        // 垂直移動
        if (playerRigidbody2D.velocity.y > minVelocityY * -1 && playerRigidbody2D.velocity.y < 0)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, minVelocityY * -1);
        }
        else if (playerRigidbody2D.velocity.y < maxVelocityY * -1)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, maxVelocityY * -1);
        }
    }

    /// <summary>
    /// 角色回復生命
    /// </summary>
    /// <param name="recoveryHp">回復生命值</param>
    public void RecoveryHp(int recoveryHp)
    {
        hp += recoveryHp;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        GameManageController.Instance.RenderHealthUI(hp, maxHp);
    }

    /// <summary>
    /// 當角色受到傷害
    /// </summary>
    /// <param name="damage">傷害值</param>
    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }
        GameManageController.Instance.RenderHealthUI(hp, maxHp);

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
