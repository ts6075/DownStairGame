using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManage : MonoBehaviour
{
    /// <summary>
    /// 左邊界
    /// </summary>
    readonly float leftBorder = -3;
    /// <summary>
    /// 右邊界
    /// </summary>
    readonly float rightBorder = 3;
    /// <summary>
    /// 起始生成位置
    /// </summary>
    readonly Vector3 initPosition = new Vector3(0, 0, 0);
    /// <summary>
    /// 最少剩餘平台數量
    /// </summary>
    readonly int minRemainGroundCnt = 10;
    /// <summary>
    /// 最多平台數量
    /// </summary>
    readonly int maxGroundCnt = 20;
    /// <summary>
    /// 目前平台清單
    /// </summary>
    private List<Transform> grounds;
    /// <summary>
    /// 平台垂直間格
    /// </summary>
    public float spacing;
    /// <summary>
    /// 角色
    /// </summary>
    public Transform player;

    void Start()
    {
        grounds = new List<Transform>();
        for (int i = 0; i < maxGroundCnt; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        RenderGround();
    }

    /// <summary>
    /// 取得新產生的平台座標
    /// </summary>
    /// <returns></returns>
    private Vector3 GetNewGroundPosition()
    {
        Vector3 vector3 = new Vector3(0, 0, 0);
        if (grounds.Count == 0)
        {
            vector3 = initPosition;
        }
        else
        {
            vector3.x = Random.Range(leftBorder, rightBorder);
            vector3.y = grounds.Last().transform.position.y - spacing;
        }
        return vector3;
    }

    /// <summary>
    /// 產生新平台
    /// </summary>
    private void SpawnGround()
    {
        GameObject newGround = Instantiate(Resources.Load<GameObject>("platform_marble"));
        newGround.transform.position = GetNewGroundPosition();
        grounds.Add(newGround.transform);
    }

    /// <summary>
    /// 重繪平台數量
    /// </summary>
    private void RenderGround()
    {
        int remainGroundCnt = grounds.Where(e => e.position.y < player.position.y).Count(); // 目前剩餘平台數量
        // 若角色下方剩餘平台數量過少,則新增平台
        if (remainGroundCnt < minRemainGroundCnt)
        {
            SpawnGround();
        }

        // 若總平台數量過多,則摧毀平台
        if (grounds.Count > maxGroundCnt)
        {
            Destroy(grounds[0].gameObject);
            grounds.RemoveAt(0);
        }
    }
}
