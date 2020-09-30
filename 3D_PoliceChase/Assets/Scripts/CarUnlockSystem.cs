using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarUnlockSystem : MonoBehaviour
{
    public int muscleUnlocked, sportUnlocked, superSportUnlocked, truckUnlocked, offRoadUnlocked, muscleSportsUnlocked;
    public int derbyUnlocked = 1;
    public GameObject selectButton, buyButton;
    private GameManager _gameManager;
    public TMP_Text price;
    public GameObject panelPrice;
    public Car[] cars;
    public string car;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        muscleUnlocked = PlayerPrefs.GetInt("muscleUnlocked", 0);
        sportUnlocked = PlayerPrefs.GetInt("sportUnlocked", 0);
        superSportUnlocked = PlayerPrefs.GetInt("superSportUnlocked", 0);
        truckUnlocked = PlayerPrefs.GetInt("truckUnlocked", 0);
        offRoadUnlocked = PlayerPrefs.GetInt("offRoadUnlocked", 0);
        muscleSportsUnlocked = PlayerPrefs.GetInt("muscleSportUnlocked", 0);
        CheckIfUnlocked();
    }
    public void CheckIfUnlocked()
    {

        if (_gameManager.index == 0)
        {
            if (derbyUnlocked == 1)
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 1)
        {
            if (muscleUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
                panelPrice.SetActive(true);
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 2)
        {
            if (sportUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
                panelPrice.SetActive(true);
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 3)
        {
            if (superSportUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
                panelPrice.SetActive(true);
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 4)
        {
            if (truckUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
                panelPrice.SetActive(true);
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 5)
        {
            if (offRoadUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                panelPrice.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
        else if (_gameManager.index == 6)
        {
            if (muscleSportsUnlocked == 0)
            {
                selectButton.SetActive(false);
                buyButton.SetActive(true);
                price.text = "$ " + cars[_gameManager.index].price.ToString();
                panelPrice.SetActive(true);
            }
            else
            {
                selectButton.SetActive(true);
                buyButton.SetActive(false);
                panelPrice.SetActive(false);
            }
        }
    }
    public void UnlockCar()
    {
        car = _gameManager.carNames[_gameManager.index];
        if (car == "Muscle")
        {
            if (_gameManager.totalMoney >= cars[1].price)
            {
                _gameManager.totalMoney -= cars[1].price;
                muscleUnlocked = 1;
                PlayerPrefs.SetInt("muscleUnlocked", muscleUnlocked);
            }
        }
        if (car == "Sport")
        {
            if (_gameManager.totalMoney >= cars[2].price)
            {
                _gameManager.totalMoney -= cars[2].price;
                sportUnlocked = 1;
                PlayerPrefs.SetInt("sportUnlocked", sportUnlocked);
            }
        }
        if (car == "SuperSport")
        {
            if (_gameManager.totalMoney >= cars[3].price)
            {
                _gameManager.totalMoney -= cars[3].price;
                superSportUnlocked = 1;
                PlayerPrefs.SetInt("superSportUnlocked", superSportUnlocked);
            }
        }
        if (car == "Truck")
        {
            if (_gameManager.totalMoney >= cars[4].price)
            {
                _gameManager.totalMoney -= cars[4].price;
                truckUnlocked = 1;
                PlayerPrefs.SetInt("truckUnlocked", truckUnlocked);
            }
        }
        if (car == "OffRoad")
        {
            if (_gameManager.totalMoney >= cars[5].price)
            {
                _gameManager.totalMoney -= cars[5].price;
                offRoadUnlocked = 1;
                PlayerPrefs.SetInt("offRoadUnlocked", offRoadUnlocked);
            }
        }
        if (car == "MuscleSport")
        {
            if (_gameManager.totalMoney >= cars[6].price)
            {
                _gameManager.totalMoney -= cars[6].price;
                muscleSportsUnlocked = 1;
                PlayerPrefs.SetInt("muscleSportUnlocked", muscleSportsUnlocked);
            }
        }
        FindObjectOfType<UIManager>().UpdatedMoneyText(_gameManager.totalMoney);
        PlayerPrefs.SetInt("money", _gameManager.totalMoney);
        CheckIfUnlocked();
    }
}
