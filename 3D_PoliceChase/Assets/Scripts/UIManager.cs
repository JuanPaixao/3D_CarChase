using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText, finalScoreText, moneyText;
    public Slider slider;
    public GameObject endGamePanel;
    public int maxHP;
    public bool gameScene;
    public void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
        slider.maxValue = this.maxHP;
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "$ " + score.ToString();
    }
    public void SetHP(int hp)
    {
        slider.value = maxHP - hp;
    }
    public void ActiveEndGamePanel(int score)
    {
        endGamePanel.SetActive(true);
        finalScoreText.text = "You got $" + score.ToString();
    }
    private void Start()
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + PlayerPrefs.GetInt("money", 0).ToString();
        }
    }
    public void UpdatedMoneyText(int money)
    {
        moneyText.text = "$ " + money.ToString();
    }
}
