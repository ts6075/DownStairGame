using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.CompareTag("DecreaseHp") && collision.gameObject.CompareTag("Player"))
        {
            PlayerController.hp -= 1;
        }
        if (this.gameObject.CompareTag("DeadHp") && collision.gameObject.CompareTag("Player"))
        {
            PlayerController.hp = 0;
        }
        Debug.Log(PlayerController.hp);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("DecreaseHp") && collision.gameObject.CompareTag("Player"))
        {
            PlayerController.hp -= 1;
        }
        if (this.gameObject.CompareTag("DeadHp") && collision.gameObject.CompareTag("Player"))
        {
            PlayerController.hp = 0;
        }
        Debug.Log(PlayerController.hp);
    }
}
