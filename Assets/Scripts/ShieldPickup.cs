using UnityEngine;
public class ShieldPickup : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ActivateShield();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                Collider2D shieldCollider = GetComponent<Collider2D>();
                if (shieldCollider != null)
                    shieldCollider.enabled = false;
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                if (sprite != null)
                    sprite.enabled = false;
                float destroyTime = 0.1f;
                if (audioSource != null && audioSource.clip != null)
                    destroyTime = audioSource.clip.length;
                Destroy(gameObject, destroyTime);
            }
        }
    }
}