using UnityEngine;
public class Coin : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                AudioSource.PlayClipAtPoint(
                    audioSource.clip,
                    transform.position
                );
            }
            Destroy(gameObject);
        }
    }
}