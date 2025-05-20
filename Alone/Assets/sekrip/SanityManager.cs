using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Untuk game over

public class SanityManager : MonoBehaviour
{
    [Header("Sanity Settings")]
    public float maxSanity = 100f;
    public float currentSanity;
    public float passiveDecreaseRate = 1f; // per detik
    public float anomalyPenalty = 5f; // jika anomaly muncul
    public float restoreAmount = 10f; // jika anomaly difoto

    [Header("UI")]
    public Slider sanitySlider;

    private bool isGameOver = false;

    void Start()
    {
        currentSanity = maxSanity;

        if (sanitySlider != null)
        {
            sanitySlider.maxValue = maxSanity;
            sanitySlider.value = currentSanity;
        }
    }

    void Update()
    {
        if (isGameOver) return;

        currentSanity -= passiveDecreaseRate * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        if (sanitySlider != null)
            sanitySlider.value = currentSanity;

        if (currentSanity <= 0)
        {
            GameOver();
        }
    }

    public void LoseSanity(float amount)
    {
        currentSanity -= amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        if (sanitySlider != null)
            sanitySlider.value = currentSanity;

        if (currentSanity <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    public void RestoreSanity(float amount)
    {
        currentSanity += amount;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        if (sanitySlider != null)
            sanitySlider.value = currentSanity;
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER - Sanity habis!");
        // Tambahkan logic game over (misal load scene, pause game, dll)
        // SceneManager.LoadScene("GameOverScene");
    }
}
