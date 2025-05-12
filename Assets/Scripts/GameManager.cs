using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score = 0;
    public bool isGameOver = false;

    [Header("UI References")]
    public GameObject gameOverMenu;      // Assigné une fois dans l’inspector
    public TextMeshProUGUI scoreText;    // Assigné dans l’inspector
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        // On se comporte comme un monobehaviour normal
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        if (scoreText != null)      scoreText.gameObject.SetActive(false);
        if (finalScoreText != null) finalScoreText.text = "Final Score: " + score;
        if (gameOverMenu != null)   gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        // Détruit ce GameManager pour recréer un tout neuf
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
