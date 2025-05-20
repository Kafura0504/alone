using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    [Header("Anomaly")]
    public GameObject[] anomalyPrefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Timing")]
    public float spawnInterval = 30f; // waktu antar spawn
    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnAnomaly();
            timer = spawnInterval;
        }
    }

    void SpawnAnomaly()
    {
        if (anomalyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("AnomalySpawner: Tidak ada prefab atau spawn point!");
            return;
        }

        // Pilih anomaly dan posisi secara acak
        GameObject anomaly = anomalyPrefabs[Random.Range(0, anomalyPrefabs.Length)];
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn instance baru
        GameObject spawned = Instantiate(anomaly, point.position, point.rotation);
        spawned.SetActive(true); // pastikan anomaly tetap aktif

        // Pastikan layer-nya benar
        spawned.layer = LayerMask.NameToLayer("Anomaly");

        // Masukkan ke daftar anomaly yang bisa dideteksi kamera
        CameraAnomaly cam = FindObjectOfType<CameraAnomaly>();
        if (cam != null)
        {
            cam.anomalyObjects.Add(spawned);
        }

    }
}
