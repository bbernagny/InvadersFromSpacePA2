using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI highscoreText;
    private int highscore;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI waveText;
    private int wave;
    public Image[] lifeSprites;
    public Image healtBar;
    public Sprite[] healthBars;
    private Color32 active = new Color(1, 1, 1, 1);
    private Color32 inactive = new Color(1, 1, 1, 0.25f);

    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    bool pauseMenuIsActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void UpdateLives(int l)
    {
        foreach(Image i in instance.lifeSprites)
        {
            i.color = instance.inactive;
        }
        for(int i = 0; i<l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    public static void UpdateHealthBar(int h)
    {
        instance.healtBar.sprite = instance.healthBars[h];
    }

    public static void UpdateScore(int s)
    {
        instance.score += s;
        instance.scoreText.text = instance.score.ToString("000,000");
    }

    public static void UpdateHighScore()
    {
        instance.highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        if(instance.score > instance.highscore)
        {
            instance.highscore = instance.score;
            PlayerPrefs.SetInt("HighScore", instance.highscore);
        }
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }

    public static void UpdateCoins()
    {
        instance.coinText.text = Inventory.currentCoins.ToString();
    }

    public static void GameOver()
    {
        instance.gameOverPanel.SetActive(true);
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseMenuIsActive = true;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseMenuIsActive = false;
    }

    public void QuitButton()
    {
        Debug.Log("work");
        Application.Quit();
    }
}
