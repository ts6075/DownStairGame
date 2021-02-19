using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManageController : MonoBehaviour
{
    /// <summary>
    /// 單一實體(Singleton)
    /// </summary>
    private static GameManageController instance = null;
    public static GameManageController Instance => instance;

    /// <summary>
    /// 角色
    /// </summary>
    public GameObject player;

    public Button restartBtn;

    /// <summary>
    /// 主UI畫布
    /// </summary>
    public GameObject uiCanvas;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        restartBtn.gameObject.SetActive(false);
        uiCanvas = GameObject.Find("Canvas");

        RenderHealthUI();
    }

    void Update()
    {
        if (PlayerController.hp <= 0)
        {
            player.SetActive(false);
            restartBtn.gameObject.SetActive(true);
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
    /// 繪製角色血量UI
    /// </summary>
    public void RenderHealthUI()
    {
        // 載入血量UI
        var hps = uiCanvas.transform.Find("HealthPoints");
        var hpimage = hps.Find("HP");

        // 先清空現有的血量UI (保留第一個血量Icon, 如果用Prefab則不用保留)
        for (int i = 1; i < hps.childCount && i < 11; i++)
        {
            GameObject.Destroy(hps.GetChild(i).gameObject);
        }

        // 根據角色血量繪製血量UI
        int healthPoint = PlayerController.hp;
        for (int i = 0; i < healthPoint; i++)
        {
            var newObj = GameObject.Instantiate(hpimage.gameObject);
            newObj.SetActive(true);
            newObj.transform.SetParent(hps);
        }
    }
}
