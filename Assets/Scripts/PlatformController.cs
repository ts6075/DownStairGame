using UnityEngine;

public class PlatformController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
    }
}
