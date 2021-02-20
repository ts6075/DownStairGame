using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformManageController : MonoBehaviour
{
    /// <summary>
    /// 平台模型
    /// </summary>
    private class PlatformModel
    {
        /// <summary>
        /// Resources名稱
        /// </summary>
        public string ResourcesName { get; set; }
        /// <summary>
        /// 出現機率權重
        /// </summary>
        public float Weight { get; set; }
    }
    /// <summary>
    /// 平台清單
    /// </summary>
    private readonly List<PlatformModel> platformList = new List<PlatformModel>()
    {
        new PlatformModel{ ResourcesName = "platform_marble", Weight = 90 },
        new PlatformModel{ ResourcesName = "platform_jelly", Weight = 10  }
    };
    /// <summary>
    /// 左邊界
    /// </summary>
    private readonly float leftBorder = -3;
    /// <summary>
    /// 右邊界
    /// </summary>
    private readonly float rightBorder = 3;
    /// <summary>
    /// 起始生成位置
    /// </summary>
    private readonly Vector3 initPosition = new Vector3(0, 0, 0);
    /// <summary>
    /// 最少剩餘平台數量
    /// </summary>
    private readonly int minRemainPlatformCnt = 10;
    /// <summary>
    /// 最多平台數量
    /// </summary>
    private readonly int maxPlatformCnt = 20;
    /// <summary>
    /// 目前平台清單
    /// </summary>
    private List<Transform> platforms;
    /// <summary>
    /// 平台垂直間格
    /// </summary>
    public float spacing;
    /// <summary>
    /// 攝影機
    /// </summary>
    public Transform mainCamera;

    void Start()
    {
        platforms = new List<Transform>();
        for (int i = 0; i < maxPlatformCnt; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        RenderPlatform();
    }

    /// <summary>
    /// 取得新產生的平台ResourcesName
    /// </summary>
    /// <returns></returns>
    private string GetNewPlatformResourcesName()
    {
        List<PlatformModel> cloneList = new List<PlatformModel>(platformList);
        List<string> randomBox = new List<string>();
        int totalWeights = 0;
        foreach(var item in cloneList)
        {
            for(var i = 0; i < item.Weight; i++)
            {
                randomBox.Add(item.ResourcesName);
                totalWeights++;
            }
        }
        int randomIndex = Random.Range(0, totalWeights);
        return randomBox[randomIndex];
    }

    /// <summary>
    /// 取得新產生的平台座標
    /// </summary>
    /// <returns></returns>
    private Vector3 GetNewPlatformPosition()
    {
        Vector3 vector3 = new Vector3(0, 0, 0);
        if (platforms.Count == 0)
        {
            vector3 = initPosition;
        }
        else
        {
            vector3.x = Random.Range(leftBorder, rightBorder);
            vector3.y = platforms.Last().transform.position.y - spacing;
        }
        return vector3;
    }

    /// <summary>
    /// 產生新平台
    /// </summary>
    private void SpawnPlatform()
    {
        string resourcesName = GetNewPlatformResourcesName();
        GameObject newPlatform = Instantiate(Resources.Load<GameObject>(resourcesName));
        newPlatform.transform.position = GetNewPlatformPosition();
        platforms.Add(newPlatform.transform);
    }

    /// <summary>
    /// 重繪平台數量
    /// </summary>
    private void RenderPlatform()
    {
        int remainPlatformCnt = platforms.Where(e => e.position.y < mainCamera.position.y).Count(); // 目前剩餘平台數量
        // 若畫面中心下方剩餘平台數量過少,則新增平台
        if (remainPlatformCnt < minRemainPlatformCnt)
        {
            SpawnPlatform();
        }

        // 若總平台數量過多,則摧毀平台
        if (platforms.Count > maxPlatformCnt)
        {
            Destroy(platforms[0].gameObject);
            platforms.RemoveAt(0);
        }
    }
}
