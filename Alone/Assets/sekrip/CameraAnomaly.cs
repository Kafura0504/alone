using System.Collections.Generic;
using UnityEngine;

public class CameraAnomaly : MonoBehaviour
{
    [Header("Anomaly Detection")]
    public List<GameObject> anomalyObjects = new List<GameObject>(); // Semua anomaly di-check dari sini
    public float detectionDistance = 15f;
    public LayerMask anomalyLayer;

    [Header("Camera Control")]
    public KeyCode toggleCameraKey = KeyCode.Mouse1; // Klik kanan
    public KeyCode snapshotKey = KeyCode.R; // Tombol ambil gambar
    public GameObject cameraUI; // Objek UI Kamera yang muncul saat aktif
    private bool cameraActive = false;

    [Header("Audio")]
    public AudioSource cameraShutterSound;

    private SanityManager sanity;


    private void Start()
    {
        sanity = FindObjectOfType<SanityManager>();

    }
    void Update()
    {
        if (Input.GetKeyDown(toggleCameraKey))
        {
            cameraActive = !cameraActive;
            cameraUI.SetActive(cameraActive); // Tampilkan UI kamera
            // Bisa tambahkan efek visual di sini
        }

        if (cameraActive && Input.GetKeyDown(snapshotKey))
        {
            TakePicture();
        }
    }

    void TakePicture()
    {
        if (cameraShutterSound != null)
        {
            cameraShutterSound.Play();
        }

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, detectionDistance, anomalyLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (anomalyObjects.Contains(hitObject))
            {
                Debug.Log("Anomaly detected: " + hitObject.name);
                hitObject.SetActive(false);

                if (sanity != null)
                    sanity.RestoreSanity(10f); // atau pakai restoreAmount dari SanityManager
            }

            else
            {
                Debug.Log("No anomaly detected.");
            }
        }
        else
        {
            Debug.Log("Nothing detected.");
        }
    }
}
