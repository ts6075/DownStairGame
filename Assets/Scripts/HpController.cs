using UnityEngine;

public class HpController : MonoBehaviour
{
    public int inputHp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inputHp > 0)
            {
                collision.gameObject.GetComponent<PlayerController>().RecoveryHp(inputHp);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().GetDamage(inputHp * -1);
            }
        }
    }
}
