using UnityEngine;
public class InfiniteGround : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject obstaclePrefab;
    public Transform player;
    public int startingPieces = 5;
    public float groundWidth = 20f;
    private float nextSpawnX = 0f;
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
        if (obstaclePrefab != null)
        {
            float obstacleX = nextSpawnX + Random.Range(3f, 7f);
            Vector3 obstaclePosition = new Vector3(
                obstacleX,
                0f,
                0f
            );
            Instantiate(
                obstaclePrefab,
                obstaclePosition,
                Quaternion.identity
            );
        }
        nextSpawnX += groundWidth;
    }
}