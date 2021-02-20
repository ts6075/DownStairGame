using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManageController : MonoBehaviour
{
    /// <summary>
    /// 單一實體(Singleton)
    /// </summary>
    private static GameManageController _instance = null;
    public static GameManageController Instance => _instance;

    /// <summary>
    /// 目前生命值清單
    /// </summary>
    private List<GameObject> hps;
    /// <summary>
    /// 主UI畫布
    /// </summary>
    public GameObject mainCanvas;
    /// <summary>
    /// 記分板
    /// </summary>
    public Text scorebord;
    /// <summary>
    /// 攝影機
    /// </summary>
    public Transform mainCamera;
    /// <summary>
    /// 角色
    /// </summary>
    public GameObject player;
    /// <summary>
    /// 重新開始按鈕
    /// </summary>
    public Button restartBtn;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        hps = new List<GameObject>();
        restartBtn.gameObject.SetActive(false);
        PlayerController playerInstance = player.GetComponent<PlayerController>();
        RenderHealthUI(playerInstance.hp, playerInstance.maxHp);
    }

    void Update()
    {
        if (hps.Count > 0)
        {
            RenderScorebord();
        }
    }

    /// <summary>
    /// 重新載入場景
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 增加生命值
    /// </summary>
    public void AddHealthPoint()
    {
        GameObject hp = Instantiate(Resources.Load<GameObject>("heartImg"));
        hp.transform.SetParent(mainCanvas.transform.Find("healthPoints"));
        hps.Add(hp);
    }

    /// <summary>
    /// 減少生命值
    /// </summary>
    private void DecreaseHealthPoint()
    {
        if (hps.Count > 0)
        {
            Destroy(hps.Last());
            hps.RemoveAt(hps.Count - 1);
        }
    }

    /// <summary>
    /// 繪製角色生命值UI
    /// </summary>
    /// <param name="remainHp">剩餘生命值</param>
    /// <param name="maxHp">最大生命值</param>
    public void RenderHealthUI(int remainHp, int maxHp)
    {
        for (int i = hps.Count; i < remainHp && i < maxHp; i++)
        {
            AddHealthPoint();
        }
        for (int i = hps.Count; i > remainHp && i >= 0; i--)
        {
            DecreaseHealthPoint();
        }
    }

    /// <summary>
    /// 繪製記分板
    /// </summary>
    private void RenderScorebord()
    {
        float mainCameraDownHeight = mainCamera.position.y;
        int downFloor = (int)mainCameraDownHeight / -7;
        scorebord.text = $"地下{downFloor:0000}層";
    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    public void GameOver()
    {
        player.SetActive(false);
        restartBtn.gameObject.SetActive(true);
    }
}
