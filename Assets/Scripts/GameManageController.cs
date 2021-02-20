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
    public GameObject uiCanvas;
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
        uiCanvas = GameObject.Find("Canvas");
        restartBtn.gameObject.SetActive(false);

        for (int i = 0; i < PlayerController.hp; i++)
        {
            AddHealthPoint();
        }
    }

    void Update()
    {
        if (PlayerController.hp <= 0)
        {
            player.SetActive(false);
            restartBtn.gameObject.SetActive(true);
        }
        RenderHealthUI();
    }

    /// <summary>
    /// 重新載入場景
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 增加血量
    /// </summary>
    private void AddHealthPoint()
    {
        GameObject hp = Instantiate(Resources.Load<GameObject>("heartImg"));
        hp.transform.SetParent(uiCanvas.transform.Find("healthPoints"));
        hps.Add(hp);
    }

    /// <summary>
    /// 減少血量
    /// </summary>
    private void DecreaseHealthPoint()
    {
        Destroy(hps.Last());
        hps.RemoveAt(hps.Count - 1);
    }

    /// <summary>
    /// 繪製角色血量UI
    /// </summary>
    public void RenderHealthUI()
    {
        // 根據角色血量繪製血量UI
        int healthPoint = PlayerController.hp;
        if (hps.Count < healthPoint)
        {
            AddHealthPoint();
        }
        else if (hps.Count > healthPoint)
        {
            DecreaseHealthPoint();
        }
    }
}
