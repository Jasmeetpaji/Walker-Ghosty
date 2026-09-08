using UnityEngine;
public class Obstacle : MonoBehaviour
{
    private Collider2D obstacleCollider;
    void Start()
    {
        obstacleCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                if (health.IsInvincible)
                    return;
                health.TakeDamage();
            }
        }
    }
}