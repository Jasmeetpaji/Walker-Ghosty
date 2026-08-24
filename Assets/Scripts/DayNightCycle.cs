using UnityEngine;
public class DayNightCycle : MonoBehaviour
{
    public Transform sun;
    public Transform moon;
    public SpriteRenderer sky;
    [Header("Cycle")]
    public float cycleDuration = 120f;
    [Header("Sun & Moon")]
    public float startX = -8f;
    public float endX = 8f;
    public float height = 4f;
    [Header("Sky Colors")]
    public Color dayColor = new Color(0.45f, 0.75f, 1f);
    public Color sunsetColor = new Color(1f, 0.35f, 0.15f);
    public Color nightColor = new Color(0.03f, 0.05f, 0.15f);
    private float timer;
    void Start()
    {
        timer = 0f;
        sun.gameObject.SetActive(true);
        moon.gameObject.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cycleDuration)
            timer = 0f;
        float time = timer / cycleDuration;
        UpdateSky(time);
        UpdateSun(time);
        UpdateMoon(time);
    }
    void UpdateSky(float time)
    {
        if (time < 0.25f)
        {
            float t = time / 0.25f;
            sky.color = Color.Lerp(
                sunsetColor,
                dayColor,
                t
            );
        }
        else if (time < 0.5f)
        {
            float t = Mathf.InverseLerp(
                0.25f,
                0.5f,
                time
            );
            sky.color = Color.Lerp(
                dayColor,
                sunsetColor,
                t
            );
        }
        else if (time < 0.75f)
        {
            float t = Mathf.InverseLerp(
                0.5f,
                0.75f,
                time
            );
            sky.color = Color.Lerp(
                sunsetColor,
                nightColor,
                t
            );
        }
        else
        {
            float t = Mathf.InverseLerp(
                0.75f,
                1f,
                time
            );
            sky.color = Color.Lerp(
                nightColor,
                sunsetColor,
                t
            );
        }
    }
    void UpdateSun(float time)
    {
        if (time < 0.5f)
        {
            sun.gameObject.SetActive(true);
            float sunTime = time / 0.5f;
            float x = Mathf.Lerp(
                startX,
                endX,
                sunTime
            );
            float y = Mathf.Sin(
                sunTime * Mathf.PI
            ) * height;

            sun.localPosition = new Vector3(
                x,
                y,
                sun.localPosition.z
            );
        }
        else
        {
            sun.gameObject.SetActive(false);
        }
    }
    void UpdateMoon(float time)
    {
        // Moon: 50% → 100%
        if (time >= 0.5f)
        {
            moon.gameObject.SetActive(true);
            float moonTime = Mathf.InverseLerp(
                0.5f,
                1f,
                time
            );
            float x = Mathf.Lerp(
                startX,
                endX,
                moonTime
            );
            float y = Mathf.Sin(
                moonTime * Mathf.PI
            ) * height;
            moon.localPosition = new Vector3(
                x,
                y,
                moon.localPosition.z
            );
        }
        else
        {
            moon.gameObject.SetActive(false);
        }
    }
}