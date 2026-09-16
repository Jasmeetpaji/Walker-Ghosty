using UnityEngine;
public class InfiniteGround : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public GameObject shieldPrefab;
    public Transform player;
    public int startingPieces = 5;
    public float groundWidth = 20f;
    [Header("Shield Settings")]
    public float shieldMinimumDistance = 300f;
    public float shieldSpawnChance = 0.25f;
    private float nextSpawnX = 0f;
    private float lastShieldX = -999f;
    private bool shieldActive = false;
    void Start()
    {
        for (int i = 0; i < startingPieces; i++)
        {
            SpawnGround();
        }
    }
    void Update()
    {
        if (player == null)
            return;
        if (player.position.x + (groundWidth * 2f) > nextSpawnX)
        {
            SpawnGround();
        }
    }
    void SpawnGround()
    {
        Vector3 groundPosition = new Vector3(
            nextSpawnX,
            -1f,
            0f
        );
        Instantiate(
            groundPrefab,
            groundPosition,
            Quaternion.identity
        );
        if (obstaclePrefabs != null &&
            obstaclePrefabs.Length > 0)
        {
            float obstacleX =
                nextSpawnX + Random.Range(3f, 7f);
            Vector3 obstaclePosition =
                new Vector3(
                    obstacleX,
                    0f,
                    0f
                );
            GameObject randomObstacle =
                obstaclePrefabs[
                    Random.Range(
                        0,
                        obstaclePrefabs.Length
                    )
                ];
            Instantiate(
                randomObstacle,
                obstaclePosition,
                Quaternion.identity
            );
        }
        if (coinPrefab != null)
        {
            float coinX =
                nextSpawnX + Random.Range(8f, 15f);
            float coinY =
                Random.Range(1.5f, 3f);
            Vector3 coinPosition =
                new Vector3(
                    coinX,
                    coinY,
                    0f
                );
            Instantiate(
                coinPrefab,
                coinPosition,
                Quaternion.identity
            );
        }
        if (shieldPrefab != null && !shieldActive)
        {
            float shieldX =
                nextSpawnX + Random.Range(10f, 18f);
            if (shieldX - lastShieldX >=
                shieldMinimumDistance)
            {
                if (Random.value < shieldSpawnChance)
                {
                    float shieldY =
                        Random.Range(1.5f, 3f);
                    Vector3 shieldPosition =
                        new Vector3(
                            shieldX,
                            shieldY,
                            0f
                        );
                    Instantiate(
                        shieldPrefab,
                        shieldPosition,
                        Quaternion.identity
                    );
                    lastShieldX = shieldX;
                    shieldActive = true;
                    Debug.Log(
                        "SHIELD SPAWNED - spawning locked"
                    );
                }
            }
        }
        nextSpawnX += groundWidth;
    }
    public void ShieldUsed()
    {
        shieldActive = false;
        Debug.Log(
            "SHIELD USED - spawning unlocked"
        );
    }
}