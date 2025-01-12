using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;   // Prefab to spawn
    public Transform topWall, bottomWall, leftWall, rightWall; // Walls of the rectangle
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Width and height of the spawn area
    public float spawnInterval = 1f;  // Time between spawns
    public float wallMoveSpeed, maxDist;
    public LineRenderer line;
    private float timeSinceLastSpawn;
    public GameObject mingame;
    private void Update()
    {
        // Update the timer
        timeSinceLastSpawn += Time.deltaTime;
        MoveWallsOut();
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f; // Reset the timer
        }
    }

    private void SpawnObject()
    {
        // Calculate random spawn position within the rectangle
        float randomX = Random.Range(-spawnAreaSize.x / 2 + 0.5f, spawnAreaSize.x / 2 - 0.5f);
        float randomZ = Random.Range(-spawnAreaSize.y / 2 + 0.5f, spawnAreaSize.y / 2 - 0.5f);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, 0, randomZ); // Spawn on the XZ plane

        var objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];

        Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(0, Random.Range(0,360f), 0));

        Minigame mhj = FindObjectOfType<Minigame>();
        if (mhj == null) {
            Instantiate(mingame, new Vector3(randomX, 1.1f, randomZ), Quaternion.Euler(0, Random.Range(0,360f), 0));
        }

    }

    public void MoveWallsOut()
    {
        spawnAreaSize.x += wallMoveSpeed * Time.deltaTime;
        spawnAreaSize.y += wallMoveSpeed * Time.deltaTime;

        spawnAreaSize.x = Mathf.Min(spawnAreaSize.x, maxDist);
        spawnAreaSize.y = Mathf.Min(spawnAreaSize.y, maxDist);

        topWall.position = Vector3.forward * spawnAreaSize.y/2;
        bottomWall.position = Vector3.back * spawnAreaSize.y/2;
        leftWall.position = Vector3.left * spawnAreaSize.x/2;
        rightWall.position = Vector3.right * spawnAreaSize.x/2;
        var x = spawnAreaSize.x/2;
        var y = spawnAreaSize.y/2;

        line.SetPositions(new Vector3[] {new Vector3(x, y, -0.1f), new Vector3(x, -y, -0.1f), new Vector3(-x, -y, -0.1f), new Vector3(-x, y, -0.1f)});
    }
    private void OnDrawGizmos()
    {
        // Set the color for the rectangle
        Gizmos.color = Color.green;

        // Draw the outline of the spawn area rectangle
        Vector3 topLeft = transform.position + new Vector3(-spawnAreaSize.x / 2, 0, spawnAreaSize.y / 2);
        Vector3 topRight = transform.position + new Vector3(spawnAreaSize.x / 2, 0, spawnAreaSize.y / 2);
        Vector3 bottomLeft = transform.position + new Vector3(-spawnAreaSize.x / 2, 0, -spawnAreaSize.y / 2);
        Vector3 bottomRight = transform.position + new Vector3(spawnAreaSize.x / 2, 0, -spawnAreaSize.y / 2);

        // Draw lines connecting corners
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
