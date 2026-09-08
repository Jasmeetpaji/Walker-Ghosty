using UnityEngine;
using TMPro;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public TextMeshProUGUI livesText;
    [Header("Invincibility")]
    public float invincibilityDuration = 3f;
    public float blinkSpeed = 0.1f;
    [Header("Shield")]
    public bool hasShield = false;
    public GameObject shieldVisual;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D[] playerColliders;
    public bool IsInvincible => isInvincible;
    void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerColliders = GetComponentsInChildren<Collider2D>();
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
        UpdateLivesUI();
    }
    public void ActivateShield()
    {
        hasShield = true;
        if (shieldVisual != null)
            shieldVisual.SetActive(true);
        Debug.Log("Shield Activated!");
    }
    public void TakeDamage()
    {
        if (isInvincible)
            return;
        if (hasShield)
        {
            hasShield = false;
            if (shieldVisual != null)
                StartCoroutine(ShieldBreakEffect());
            StartCoroutine(Invincibility());
            return;
        }
        currentLives--;
        UpdateLivesUI();
        if (currentLives <= 0)
        {
            GameManager.instance.GameOver();
            return;
        }
        StartCoroutine(Invincibility());
    }
    IEnumerator ShieldBreakEffect()
    {
        if (shieldVisual == null)
            yield break;
        SpriteRenderer shieldRenderer =
            shieldVisual.GetComponent<SpriteRenderer>();
        if (shieldRenderer == null)
        {
            shieldVisual.SetActive(false);
            yield break;
        }
        Vector3 originalScale = shieldVisual.transform.localScale;
        float timer = 0f;
        float effectDuration = 0.3f;
        while (timer < effectDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / effectDuration;
            shieldVisual.transform.localScale =
                Vector3.Lerp(
                    originalScale,
                    originalScale * 1.5f,
                    progress
                );
            Color color = shieldRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, progress);
            shieldRenderer.color = color;
            yield return null;
        }
        shieldVisual.transform.localScale = originalScale;
        Color resetColor = shieldRenderer.color;
        resetColor.a = 1f;
        shieldRenderer.color = resetColor;
        shieldVisual.SetActive(false);
    }
    IEnumerator Invincibility()
    {
        isInvincible = true;
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            Collider2D obstacleCollider =
                obstacle.GetComponent<Collider2D>();
            if (obstacleCollider != null)
            {
                foreach (Collider2D playerCollider in playerColliders)
                {
                    Physics2D.IgnoreCollision(
                        playerCollider,
                        obstacleCollider,
                        true
                    );
                }
            }
        }
        float timer = 0f;
        while (timer < invincibilityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkSpeed);
            timer += blinkSpeed * 2f;
        }
        spriteRenderer.enabled = true;
        obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            Collider2D obstacleCollider =
                obstacle.GetComponent<Collider2D>();
            if (obstacleCollider != null)
            {
                foreach (Collider2D playerCollider in playerColliders)
                {
                    Physics2D.IgnoreCollision(
                        playerCollider,
                        obstacleCollider,
                        false
                    );
                }
            }
        }
        isInvincible = false;
    }
    void UpdateLivesUI()
    {
        if (currentLives == 3)
        {
            livesText.text = "❤️❤️❤️";
        }
        else if (currentLives == 2)
        {
            livesText.text = "❤️❤️";
        }
        else if (currentLives == 1)
        {
            livesText.text = "❤️";
        }
        else
        {
            livesText.text = "";
        }
    }
}