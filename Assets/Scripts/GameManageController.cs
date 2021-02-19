using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManageController : MonoBehaviour
{
    /// <summary>
    /// 角色
    /// </summary>
    public GameObject player;

    public Button restartBtn;

    void Start()
    {
        restartBtn.gameObject.SetActive(false);
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
}
