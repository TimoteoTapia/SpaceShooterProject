using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Text scoreText, livesText;

    public void UpdateLives(int lives)
    {
        livesText.text = "x" + lives;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateHealthBar(float healthMax, float health)
    {
        healthBar.fillAmount = (1 / healthMax) * health;
    }
}

