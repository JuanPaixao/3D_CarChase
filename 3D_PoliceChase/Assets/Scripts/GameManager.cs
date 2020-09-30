using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] moneyBags, carCops;
    public float timeToRespawn;
    private float startTimeToRespawn;
    public int score;
    private UIManager _uiManager;
    public int copsActive, minActive;
    public int totalMoney;
    public bool finished;
    public GameObject[] carSelected;
    private CarSelected _carSelected;
    public string[] carNames;
    public int index;
    public CarUnlockSystem carUnlockSystem;
    public bool shop, game;
    public GameObject pausePanel;
    private void Awake()
    {
        _carSelected = FindObjectOfType<CarSelected>();
    }
    private void Start()
    {
        moneyBags = GameObject.FindGameObjectsWithTag("MoneyBag");
        carCops = GameObject.FindGameObjectsWithTag("Cop");
        startTimeToRespawn = timeToRespawn;
        _uiManager = FindObjectOfType<UIManager>();
        copsActive = carCops.Length;
        index = PlayerPrefs.GetInt("index", 0);
        LoadCar();
        if (shop)
        {
            carUnlockSystem.CheckIfUnlocked();
        }
        totalMoney = PlayerPrefs.GetInt("money", 0);
        Time.timeScale = 1;
    }
    private void Update()
    {
        timeToRespawn -= Time.deltaTime;
        if (timeToRespawn <= 0)
        {
            foreach (var item in moneyBags)
            {
                item.SetActive(true);
            }
            timeToRespawn = startTimeToRespawn;
        }
        if (game)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

    }
    public void UpdateMoney(int money)
    {
        totalMoney = PlayerPrefs.GetInt("money", 0);
        this.totalMoney += money;
        PlayerPrefs.SetInt("money", totalMoney);
    }
    public void AddScore()
    {
        score += 100;
        _uiManager.SetScoreText(score);
    }
    public void CopSystem()
    {
        copsActive--;
        if (GameObject.FindGameObjectsWithTag("Cop").Length < minActive)
        {

        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void EndGame()
    {
        if (!finished)
        {
            _uiManager.ActiveEndGamePanel(score);
            UpdateMoney(score);
            Debug.Log(totalMoney);
            finished = true;
        }
    }

    public void AddIndex()
    {
        if (index < carSelected.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        ChangeCar();
    }
    public void SubtractIndex()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = carSelected.Length - 1;
        }
        ChangeCar();
    }
    public void SelectCar()
    {
        PlayerPrefs.SetInt("index", index);
        _carSelected.UpdateCar(_carSelected.carSelected);
    }
    public void ChangeCar()
    {
        foreach (GameObject car in carSelected)
        {
            car.SetActive(false);
        }
        carSelected[index].SetActive(true);
        switch (index)
        {
            case 0:
                _carSelected.carSelected = "Derby";
                break;
            case 1:
                _carSelected.carSelected = "Muscle";
                break;

            case 3:
                _carSelected.carSelected = "SuperSport";
                break;

            case 2:
                _carSelected.carSelected = "Sport";
                break;

            case 4:
                _carSelected.carSelected = "Truck";
                break;

            case 5:
                _carSelected.carSelected = "OffRoad";
                break;
            case 6:
                _carSelected.carSelected = "MuscleSport";
                break;
        }
        carUnlockSystem.CheckIfUnlocked();
    }
    public void StartGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        LoadScene("Scene01");
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }
    public void LoadCar()
    {
        if (_carSelected != null)
        {
            Debug.Log(_carSelected.carSelected);
            if (_carSelected.carSelected == "Derby")
            {
                carSelected[0].SetActive(true);
            }
            if (_carSelected.carSelected == "Muscle")
            {
                carSelected[1].SetActive(true);
            }
            if (_carSelected.carSelected == "SuperSport")
            {
                carSelected[3].SetActive(true);
            }
            if (_carSelected.carSelected == "Sport")
            {
                carSelected[2].SetActive(true);
            }
            if (_carSelected.carSelected == "Truck")
            {
                carSelected[4].SetActive(true);
            }
            if (_carSelected.carSelected == "OffRoad")
            {
                carSelected[5].SetActive(true);
            }
            if (_carSelected.carSelected == "MuscleSport")
            {
                carSelected[6].SetActive(true);
            }
        }
    }
}
