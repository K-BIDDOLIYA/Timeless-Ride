using System.Collections.Generic;
using UnityEngine;

// Streams ground chunks ahead of the car and deletes old ones behind it.
// Ground height comes from summed sine waves whose amplitude/frequency
// grow with distance, so the track gets rougher the further you go.
// Also spawns Checkpoint and Diamond prefabs as it builds each chunk.
public class TerrainGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform car;
    public GameObject groundChunkPrefab;   // needs EdgeCollider2D + LineRenderer (see notes)
    public GameObject checkpointPrefab;    // needs BoxCollider2D (isTrigger) + Checkpoint.cs
    public GameObject diamondPrefab;       // needs CircleCollider2D (isTrigger) + Diamond.cs

    [Header("Terrain Shape")]
    public float pointSpacing = 0.5f;
    public int pointsPerChunk = 60;
    public float baseAmplitude = 2f;
    public float baseFrequency = 0.15f;
    public float difficultyRampPerMeter = 0.0008f; // how fast it gets "crazier"
    public float groundYOffset = -2f;

    [Header("Spawning")]
    public float checkpointInterval = 40f;   // meters between checkpoints
    public float diamondChancePerChunk = 0.6f; // rough odds a chunk gets a diamond
    public float diamondHeightAboveGround = 1.5f;

    [Header("Streaming Distance")]
    public float generateAheadDistance = 100f;
    public float destroyBehindDistance = 60f;

    private float lastGeneratedX = 0f;
    private float lastCheckpointX = 0f;
    private readonly List<GameObject> activeChunks = new List<GameObject>();

    void Start()
    {
        // Flat starting stretch so the car doesn't spawn on a slope.
        GenerateChunk(startFlat: true);
        while (lastGeneratedX < car.position.x + generateAheadDistance)
            GenerateChunk();
    }

    void Update()
    {
        while (lastGeneratedX < car.position.x + generateAheadDistance)
            GenerateChunk();

        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            GameObject chunk = activeChunks[i];
            if (chunk == null) { activeChunks.RemoveAt(i); continue; }

            EdgeCollider2D ec = chunk.GetComponent<EdgeCollider2D>();
            float chunkEndX = chunk.transform.position.x + ec.points[ec.points.Length - 1].x;

            if (chunkEndX < car.position.x - destroyBehindDistance)
            {
                Destroy(chunk);
                activeChunks.RemoveAt(i);
            }
        }
    }

    // The height function: this is the "sine waves that go crazy" part.
    float GetGroundHeight(float x)
    {
        float amp = baseAmplitude + x * difficultyRampPerMeter * 3f;
        float freq = baseFrequency + x * difficultyRampPerMeter;

        // Sum a few sine waves at different frequencies/phases so the
        // profile isn't a single repeating hump — it gets bumpier and
        // less predictable with distance.
        float y = Mathf.Sin(x * freq) * amp;
        y += Mathf.Sin(x * freq * 2.3f + 1.7f) * amp * 0.35f;
        y += Mathf.Sin(x * freq * 0.5f + 4.1f) * amp * 0.5f;

        return y + groundYOffset;
    }

    void GenerateChunk(bool startFlat = false)
    {
        GameObject chunk = Instantiate(groundChunkPrefab, Vector3.zero, Quaternion.identity, transform);
        EdgeCollider2D edge = chunk.GetComponent<EdgeCollider2D>();
        LineRenderer line = chunk.GetComponent<LineRenderer>();
        // In the chunk prefab, set LineRenderer > Use World Space = OFF,
        // so these local points line up with the collider's local points.

        Vector2[] points = new Vector2[pointsPerChunk];
        Vector3[] linePoints = new Vector3[pointsPerChunk];
        float chunkStartX = lastGeneratedX;

        for (int i = 0; i < pointsPerChunk; i++)
        {
            float worldX = chunkStartX + i * pointSpacing;
            float y = (startFlat && worldX < 15f) ? groundYOffset : GetGroundHeight(worldX);

            points[i] = new Vector2(worldX - chunkStartX, y);
            linePoints[i] = new Vector3(worldX - chunkStartX, y, 0f);

            if (!startFlat && diamondPrefab != null &&
                Random.value < diamondChancePerChunk / pointsPerChunk)
            {
                Instantiate(diamondPrefab, new Vector3(worldX, y + diamondHeightAboveGround, 0f), Quaternion.identity);
            }
        }

        chunk.transform.position = new Vector3(chunkStartX, 0f, 0f);
        edge.points = points;
        line.positionCount = pointsPerChunk;
        line.SetPositions(linePoints);

        activeChunks.Add(chunk);
        lastGeneratedX = chunkStartX + (pointsPerChunk - 1) * pointSpacing;

        if (!startFlat && checkpointPrefab != null && lastGeneratedX - lastCheckpointX >= checkpointInterval)
        {
            float cpX = lastCheckpointX + checkpointInterval;
            float cpY = GetGroundHeight(cpX);
            Instantiate(checkpointPrefab, new Vector3(cpX, cpY + 2f, 0f), Quaternion.identity);
            lastCheckpointX = cpX;
        }
    }
}
