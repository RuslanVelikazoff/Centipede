using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Blaster blaster;
    private Centipede centipede;
    private MushroomField mushroomField;

    public GameObject gameOver;
    public Text scoreText;
    public Text livesText;

    public GameObject gamePanel;

    private int score;
    private int lives;

    private bool isGameOver;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        blaster = FindObjectOfType<Blaster>();
        centipede = FindObjectOfType<Centipede>();
        mushroomField = FindObjectOfType<MushroomField>();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown && isGameOver) {
            SceneManager.LoadScene(0);
        }
    }

    public void NewGame()
    {
        gamePanel.SetActive(true);

        SetScore(0);
        SetLives(3);

        blaster.Respawn();
        centipede.Respawn();
        mushroomField.Clear();
        mushroomField.Generate();
        gameOver.SetActive(false);
    }

    public void PauseGame()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        SceneManager.LoadScene(0);
    }

    public void ResetRound()
    {
        AudioManager.instance.Play("Lose");
        SetLives(lives - 1);

        if (lives <= 0)
        {
            GameOver();
            return;
        }

        mushroomField.Heal();
        centipede.Respawn();
        blaster.Respawn();
    }

    private void GameOver()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        isGameOver = true;
        gameOver.SetActive(true);
        blaster.gameObject.SetActive(false);
    }

    public void IncreaseScore(int amount)
    {
        SetScore(score + amount);
    }

    public void NextLevel()
    {
        centipede.speed *= 1.1f;
        centipede.Respawn();
    }

    private void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }

    private void SetLives(int value)
    {
        lives = value;
        livesText.text = "Lives: " + lives.ToString();
    }

}
